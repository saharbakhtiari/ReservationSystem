using System;

namespace Domain.Users
{
    public class PermissionDto
    {
        public Guid Id { get; set; }

        // کلید سیستمی (انگلیسی)
        public string Code { get; set; }

        // عنوان فارسی برای نمایش
        public string Title { get; set; }

        //// اگر بخوای درختی نمایش بدی
        //public string ParentCode { get; set; }
        //public int Level { get; set; }
    }
}