using Application.Common.Security;
using MediatR;

namespace Application.UserManagers.Commands.EditPhoneNumber
{
    [Authorize]
    public class EditPhoneNumberCommand : IRequest
    {
        public string PhoneNumber { get; set; }
        public string VerifyCode { get; set; }
    }
}