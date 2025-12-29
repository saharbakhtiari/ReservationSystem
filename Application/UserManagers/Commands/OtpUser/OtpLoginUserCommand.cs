using Domain.Users;
using MediatR;

namespace Application.UserManagers.Commands.OtpUser
{
    public class OtpLoginUserCommand : IRequest
    {
        public string PhoneNumber { get; set; }
    }
}