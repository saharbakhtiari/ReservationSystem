using Application.Common.Dtos;
using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Application.Books.Commands.CreateBook
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_BookManager)]
    public class CreateBookCommand : IRequest<long>
    {
        [FromForm]
        public string Title { get; set; } = null!;
        [FromForm]
        public IFormFile Image { get; set; }

    }
}
