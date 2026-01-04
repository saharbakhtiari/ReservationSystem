using Domain.Common.Mappings;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using System;

namespace Infrastructure.UserAccount
{
    /// <summary>
    /// Application user object used by the infrastructure layer.
    /// Use mapping to convert this to Domain Model equivalent if need be.
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid> ,IMapTo<UserInputDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DistinguishedName { get; set; }
        public string Sex { get; set; }
        public string EmployeeNumber { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string BirthDate { get; set; }
        public Guid? CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedUtc { get; set; }
        public Guid? LastModifiedById { get; set; }
        public string LastModifiedByName { get; set; }
        public DateTime? LastModifiedUtc { get; set; }
        /// <summary>
        /// Last recorded moment where the user interacted with the system
        /// </summary>
        public DateTime LastActiveTimeUtc { get; set; }
        public LoginProvider LoginProvider { get; set; }
        public Guid UserKey { get; set; }
    }
}
