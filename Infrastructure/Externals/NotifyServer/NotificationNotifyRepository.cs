using Domain.Common;
using Domain.Contract.Common;
using Domain.Externals.NotifyServer;
using Domain.Settings;
using Domain.UnitOfWork.Uow;
using Exceptions;
using Extensions;
using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Externals.NotifyServer;

public partial class NotificationNotifyRepository : GenericApiCallerRepository<Notification>, INotificationRepository
{
    private readonly INotifyServerConfiguration _notifyServerconfiguration;
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public NotificationNotifyRepository(IApiContext context, INotifyServerConfiguration notifyServerconfiguration, IConfiguration configuration, IUnitOfWorkManager unitOfWorkManager) : base(context)
    {
        _notifyServerconfiguration = notifyServerconfiguration;
        _configuration = configuration;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task Login(CancellationToken cancellationToken)
    {
        var url = $"{_notifyServerconfiguration.EndpointAddress}/{NotifyServerApis.LoginNotify}";
        LoginNotifyServer login = new()
        {

            UserName = _notifyServerconfiguration.UserName,
            Password = _notifyServerconfiguration.Password
        };

        var token = await login.Repository.CallService<Token>(url);
        await SaveToken(token, cancellationToken);
    }

    public async Task RefreshToken(CancellationToken cancellationToken)
    {
        var url = $"{_notifyServerconfiguration.EndpointAddress}/{NotifyServerApis.RefreshTokenNotify}";
        string refreshToken = "";
        using (var uow = _unitOfWorkManager.Begin(new SedUnitOfWorkOptions { IsTransactional = false }, requiresNew: true))
        {
            refreshToken = (await new Setting().Repository.GetSettingByKeyAsync(SettingKeys.NotificationRefreshToken, cancellationToken)).Value.Decrypt(_configuration.GetSection("EncyptionKey").Value);
        }
        RefreshTokenNotifyServer refreshTokenNotify = new()
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
            await new Setting().DomainService.Upsert(SettingKeys.NotificationRefreshToken, encryptToken, cancellationToken);
        }
    }

    public async Task<TResponse> SendAsync<TResponse>(string path, CancellationToken cancellationToken)
    {
        var url = $"{_notifyServerconfiguration.EndpointAddress}/{path}";
        if (GetAccessToken().IsNullOrEmpty())
        {
            await Login(cancellationToken);
        }
        try
        {
            return await OwnerEntity.Repository.CallService<TResponse>(url);
        }
        catch (ExternalServiceException ex)
        {
            if (ex.StatusCode == 401)
            {
                await RefreshToken(cancellationToken);
                return await OwnerEntity.Repository.CallService<TResponse>(url);
            }
            else
            {
                throw;
            }
        }
        catch (Exception ex)
        {
            throw new ExternalServiceException($"Notification : {ex.ToJson()}");
        }
    }
}

public class Token
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string ExpireDate { get; set; }
}

