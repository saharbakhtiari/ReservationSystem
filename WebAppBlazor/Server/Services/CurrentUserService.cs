using Domain.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace WebAppBlazor.Server.Services
{
    public class CurrentUserService : ICurrentUserService
    {

        // Bug with this clean architecture strategy with MVC applications:
        // https://github.com/jasontaylordev/CleanArchitecture/issues/132#issuecomment-631357951
        // remedy by lazy-loading the user ID

        private IHttpContextAccessor _httpContextAccessor;
        private bool _initId = false;
        private bool _initName = false;
        private bool _initEmail = false;
        private bool _initFirstName = false;
        private bool _initLastName = false;
        private Guid? _userId;
        private string? _name;
        private string? _emailAddress;
        private string? _firstName;
        private string? _lastName;

        public Guid? UserId
        {
            get
            {
                if (_initId == false)
                {
                    string? userIdString = _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                    
                    //var userIdStringFromCookie = _httpContextAccessor?.HttpContext?.Request.Cookies["UserId"];
                    //if (userIdStringFromCookie != userIdString)
                    //{
                    //    _userId = null;
                    //    _initId = true;
                    //    return _userId;
                    //}

                    _userId = string.IsNullOrWhiteSpace(userIdString) ? null : Guid.Parse(userIdString);

                    _initId = true;
                }

                return _userId;
            }
        }

        public string UserName
        {
            get
            {
                if (_initName == false)
                {
                    _name = _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
                    _initName = true;
                }

                return _name;
            }
        }

        public string EmailAddress
        {
            get
            {
                if (_initEmail == false)
                {
                    _emailAddress = _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
                    _initEmail = true;
                }

                return _emailAddress;

            }
        }

        public string FirstName
        {
            get
            {
                if (_initFirstName == false)
                {
                    _firstName = _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.GivenName);
                    _initFirstName = true;
                }

                return _firstName;
            }
        }

        public string LastName
        {
            get
            {
                if (_initLastName == false)
                {
                    _lastName = _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.Surname);
                    _initLastName = true;
                }

                return _lastName;
            }
        }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

    }
}
