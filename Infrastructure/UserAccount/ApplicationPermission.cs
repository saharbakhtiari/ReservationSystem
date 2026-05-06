using Infrastructure.UserAccount.Permission;
using System;

namespace Infrastructure.UserAccount
{
    public class ApplicationPermission
        : IdentityPermission<Guid, ApplicationPermission, ApplicationUserRoles>
    {
        /// <summary>
        /// کلید سیستمی یکتا (انگلیسی)
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationPermission() : base()
        {
        }

        /// <summary>
        /// سازنده اصلی جدید
        /// </summary>
        public ApplicationPermission(string code, string title) : base(title)
        {
            Code = code;
            Name = title; // Name برای نمایش فارسی
        }
    }
}