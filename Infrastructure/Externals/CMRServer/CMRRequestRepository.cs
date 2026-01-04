using Domain.Common;
using Domain.Contract.Common;
using Domain.Externals.CMRServer;
using Domain.Externals.CMRServer;
using Domain.Settings;
using Domain.UnitOfWork.Uow;
using Exceptions;
using Extensions;
using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Externals.CMRServer;

public partial class CMRRequestRepository : GenericApiCallerRepository<CMRRequest>, ICMRRequestRepository
{
    private readonly ICMRServerConfiguration _CMRServerconfiguration;
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public CMRRequestRepository(IApiContext context, ICMRServerConfiguration CMRServerconfiguration, IConfiguration configuration, IUnitOfWorkManager unitOfWorkManager) : base(context)
    {
        _CMRServerconfiguration = CMRServerconfiguration;
        _configuration = configuration;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task Login(CancellationToken cancellationToken)
    {
        var url = $"{_CMRServerconfiguration.EndpointAddress}/{CMRServerApis.Login}";
        LoginCMRServer login = new()
        {

            UserName = _CMRServerconfiguration.UserName,
            Password = _CMRServerconfiguration.Password
        };

        var token = await login.Repository.CallService<Token>(url);
        await SaveToken(token, cancellationToken);
    }

    public async Task RefreshToken(CancellationToken cancellationToken)
    {
        var url = $"{_CMRServerconfiguration.EndpointAddress}/{CMRServerApis.RefreshToken}";
        string refreshToken = "";
        using (var uow = _unitOfWorkManager.Begin(new SedUnitOfWorkOptions { IsTransactional = false }, requiresNew: true))
        {
            refreshToken = (await new Setting().Repository.GetSettingByKeyAsync(SettingKeys.CMRServerRefreshToken, cancellationToken)).Value.Decrypt(_configuration.GetSection("EncyptionKey").Value);
        }
        RefreshTokenCMRServer refreshTokenNotify = new()
        {
            AccessToken = GetAccessToken(),
            RefreshToken = refreshToken
        };
        var token = await refreshTokenNotify.Repository.CallService<Token>(url);
        await SaveToken(token, cancellationToken);
    }
    private async Task SaveToken(Token token, CancellationToken cancellationToken)
    {
        if (token is not null)
        {
            SetAccessToken(token.AccessToken);
            var encryptToken = token.RefreshToken.Encrypt(_configuration.GetSection("EncyptionKey").Value);
            await new Setting().DomainService.Upsert(SettingKeys.CMRServerRefreshToken, encryptToken, cancellationToken);
        }
    }

    public async Task<TResponse> SendGetAsync<TResponse>(string path, CancellationToken cancellationToken)
    {
        var url = $"{_CMRServerconfiguration.EndpointAddress}/{path}";
        //if (GetAccessToken().IsNullOrEmpty())
        //{
        //    await Login(cancellationToken);
        //}
        try
        {
            return await OwnerEntity.Repository.CallGetService<TResponse>(url);
        }
        catch (ExternalServiceException ex)
        {
            //if (ex.StatusCode == 401)
            //{
            //    await RefreshToken(cancellationToken);
            //    return await OwnerEntity.Repository.CallService<TResponse>(url);
            //}
            //else
            //{
                throw;
            //}
        }
        catch (Exception ex)
        {
            throw new ExternalServiceException($"CMRRequest : {ex.ToJson()}");
        }
    }
}

public class Token
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string ExpireDate { get; set; }
}

