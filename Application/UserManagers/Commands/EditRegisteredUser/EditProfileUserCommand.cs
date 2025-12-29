using Application.Common.Security;
using MediatR;
using System;

namespace Application.UserManagers.Commands.EditRegisteredUser
{
    [Authorize]
    public class EditProfileUserCommand : IRequest
    {
       // public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeNumber { get; set; }
        public string Email { get; set; }
    }
}