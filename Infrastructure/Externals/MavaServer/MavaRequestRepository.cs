using Domain.Common;
using Domain.Contract.Common;
using Domain.Externals.MavaServer;
using Domain.Settings;
using Domain.UnitOfWork.Uow;
using Exceptions;
using Extensions;
using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Externals.MavaServer;

public partial class MavaRequestRepository : GenericApiCallerRepository<MavaRequest>, IMavaRequestRepository
{
    private readonly IMavaServerConfiguration _MavaServerconfiguration;
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public MavaRequestRepository(IApiContext context, IMavaServerConfiguration MavaServerconfiguration, IConfiguration configuration, IUnitOfWorkManager unitOfWorkManager) : base(context)
    {
        _MavaServerconfiguration = MavaServerconfiguration;
        _configuration = configuration;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task Login(CancellationToken cancellationToken)
    {
        var url = $"{_MavaServerconfiguration.EndpointAddress}/{MavaServerApis.Login}";
        LoginMavaServer login = new()
        {

            UserName = _MavaServerconfiguration.UserName,
            Password = _MavaServerconfiguration.Password
        };

        var token = await login.Repository.CallService<Token>(url);
        await SaveToken(token, cancellationToken);
    }
    public async Task RefreshToken(CancellationToken cancellationToken)
    {
        var url = $"{_MavaServerconfiguration.EndpointAddress}/{MavaServerApis.RefreshToken}";
        string refreshToken = "";
        using (var uow = _unitOfWorkManager.Begin(new SedUnitOfWorkOptions { IsTransactional = false }, requiresNew: true))
        {
            refreshToken = (await new Setting().Repository.GetSettingByKeyAsync(SettingKeys.MavaServerRefreshToken, cancellationToken)).Value.Decrypt(_configuration.GetSection("EncyptionKey").Value);
        }
        RefreshTokenMavaServer refreshTokenNotify = new()
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
            await new Setting().DomainService.Upsert(SettingKeys.MavaServerRefreshToken, encryptToken, cancellationToken);
        }
    }

    public async Task<TResponse> SendGetAsync<TResponse>(string path, CancellationToken cancellationToken)
    {
        var url = $"{_MavaServerconfiguration.EndpointAddress}/{path}";
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
            throw new ExternalServiceException($"MavaRequest : {ex.ToJson()}");
        }
    }
}

public class Token
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string ExpireDate { get; set; }
}

