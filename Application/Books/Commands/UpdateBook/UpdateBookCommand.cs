using Application.Common.Dtos;
using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Application.Books.Commands.UpdateBook
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_NewsManager)]
    public class UpdateBookCommand : IRequest
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        [FromForm]
        public IFormFile? Image { get; set; }
    }
}
