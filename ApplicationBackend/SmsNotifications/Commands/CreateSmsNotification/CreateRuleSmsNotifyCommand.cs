using Application.SmsNotifications.Commands.CreateSmsNotification;
using Application_Backend.Common;
using Domain.MemberProfiles;
using Domain.Common.Interfaces;
using Domain.Externals.NotifyServer.EmailNotifications;
using Exceptions;
using Extensions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.EmailNotifications.Commands.CreateEmailNotification;

public class CreateRuleSmsNotifyCommandHandler : IRequestHandler<CreateRuleSmsNotifyCommand, bool>
{
    private readonly IStringLocalizer _localizer;
    private readonly ICurrentUserService _currentUserService;
    public CreateRuleSmsNotifyCommandHandler(IStringLocalizer localizer, ICurrentUserService currentUserService)
    {
        _localizer = localizer;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(CreateRuleSmsNotifyCommand request, CancellationToken cancellationToken)
    {
        var profile = _currentUserService.UserId.HasValue ?
                                    await MemberProfile.GetIncludedProfileAsync(_currentUserService.UserId.Value, cancellationToken) :
                                    throw new UserFriendlyException(_localizer["User is not login"]);
        if (profile is null)
        {
            throw new UserFriendlyException(_localizer["User profile is not defined"]);
        }
        if (profile.PhoneNumber.IsNullOrEmpty())
        {
            throw new UserFriendlyException(_localizer["There is no phone number registered for the user"]);
        }
        CreateSmsNotify createNotify = new()
        {
            PhoneNumber = profile.PhoneNumber,
            Key = $"{NotificationStatics.RuleKey}{request.RuleId}"
        };
        await createNotify.SendAsync(cancellationToken);
        return true;
    }
}
