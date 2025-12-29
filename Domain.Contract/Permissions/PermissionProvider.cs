using System.Collections.Generic;
using System.Linq;

namespace Domain.Permissions
{
    public class PermissionProvider
    {
        public record Permission(string PermissionName, string ParentName = null)
        {
            public int Level => Parent?.Level + 1 ?? 0;
            public Permission Parent => ParentName is null ? null : Permissions.First(x => x.PermissionName == ParentName);
            public IEnumerable<Permission> Children => Permissions.Where(x => x.ParentName == PermissionName);
        };

        public readonly static List<Permission> Permissions = new()
        {
            new (PermissionNames.Manager),

            new (PermissionNames.Manager_UserManager,PermissionNames.Manager),
            //new (PermissionNames.Manager_UserManager_Create,PermissionNames.Manager_UserManager),
            // new (PermissionNames.Manager_UserManager_Delete,PermissionNames.Manager_UserManager),
            //new (PermissionNames.Manager_UserManager_Edit,PermissionNames.Manager_UserManager),
            //new (PermissionNames.Manager_UserManager_Read,PermissionNames.Manager_UserManager),
            // new (PermissionNames.Manager_UserManager_ManageRole,PermissionNames.Manager_UserManager),

            new (PermissionNames.Manager_RuleManager,PermissionNames.Manager),
            //new (PermissionNames.Manager_RuleManager_Create,PermissionNames.Manager_RuleManager),
            //new (PermissionNames.Manager_RuleManager_Delete,PermissionNames.Manager_RuleManager),
            // new (PermissionNames.Manager_RuleManager_Edit,PermissionNames.Manager_RuleManager),
            //new (PermissionNames.Manager_RuleManager_Read,PermissionNames.Manager_RuleManager),
 
            new (PermissionNames.Manager_RoleManager,PermissionNames.Manager),
            // new (PermissionNames.Manager_RoleManager_Create,PermissionNames.Manager_RoleManager),
            // new (PermissionNames.Manager_RoleManager_Delete,PermissionNames.Manager_RoleManager),
            // new (PermissionNames.Manager_RoleManager_Edit,PermissionNames.Manager_RoleManager),
            // new (PermissionNames.Manager_RoleManager_Read,PermissionNames.Manager_RoleManager),
            // new (PermissionNames.Manager_RoleManager_ManagePermission,PermissionNames.Manager_RoleManager),
            new (PermissionNames.Manager_RulePublishment,PermissionNames.Manager),

            new (PermissionNames.Manager_RuleSurveyManager,PermissionNames.Manager),

            new (PermissionNames.Manager_InvitationManager,PermissionNames.Manager),

            new (PermissionNames.Manager_SliderManager,PermissionNames.Manager),

            new (PermissionNames.Manager_RuleQuestionManager,PermissionNames.Manager),
            new (PermissionNames.Manager_RuleSurvey_Comment_Write),
                new (PermissionNames.Manager_RuleSurveyView),
           // new (PermissionNames.Manager_RuleSurvey_Comment_Read,PermissionNames.Manager),


        new (PermissionNames.Manager_BaseInformation),
            new(PermissionNames.Manager_BaseInformation_Tags, PermissionNames.Manager_BaseInformation),
                          new(PermissionNames.Manager_PageManager),
                            new(PermissionNames.Manager_AnnouncementManager),
                             new(PermissionNames.Manager_NewsManager)








    };
    }
    public static class PermissionNames
    {
        public const string Manager = "مدیر اطلاعات";

        public const string Manager_UserManager = "مدیریت کاربران";
        //public const string Manager_UserManager_Create = "ایجاد کاربر جدید";
        //public const string Manager_UserManager_Delete = "حذف کاربر";
        //public const string Manager_UserManager_Edit = "ویرایش کاربر";
        //public const string Manager_UserManager_Read = "خواندن کاربران";
        //public const string Manager_UserManager_ManageRole = "مدیریت نقش های کاربر";

        public const string Manager_RoleManager = "مدیریت نقش ها";
        //public const string Manager_RoleManager_Create = "ایجاد نقش جدید";
        //public const string Manager_RoleManager_Delete = "حذف نقش";
        //public const string Manager_RoleManager_Edit = "ویرایش نقش";
        //public const string Manager_RoleManager_Read = "خواندن نقش";
        //public const string Manager_RoleManager_ManagePermission = "مدیریت مجوز های یک نقش";



        public const string Manager_RuleManager = "مدیریت  قوانین";
        //public const string Manager_RuleManager_Create = "ایجاد قانون";
        //public const string Manager_RuleManager_Delete = "حذف قانون";
        //public const string Manager_RuleManager_Edit = "ویرایش قانون";
        //public const string Manager_RuleManager_Read = "خواندن قوانین";
        public const string Manager_RulePublishment = "انتشار قوانین";

        public const string Manager_RuleQuestionManager = "مدیریت  پرسش و پاسخ";


        public const string Manager_PageManager = "مدیریت  صفحات";

        public const string Manager_AnnouncementManager = "مدیریت  اطلاعیه ها";

        public const string Manager_NewsManager = "مدیریت  اخبار ";
       
        

        public const string Manager_SliderManager = "مدیریت  اسلایدر ";

        public const string Manager_RuleSurveyManager = "مدیریت  نظرسنجی ";
        public const string Manager_RuleSurveyView= "مشاهده نظرسنجی ها ";
        public const string Manager_RuleSurvey_Comment_Write = "شرکت در نظرسنجی ";
        // public const string Manager_RuleSurvey_Comment_Read = "دسترسی خواندن نظر ";

        public const string Manager_InvitationManager = "مدیریت  فراخوان ";
        public const string Manager_InvitationView = "مشاهده فراخوان ها ";
        public const string Manager_Invitation_Comment_Write = "شرکت در فراخوان ";


        public const string Manager_BaseInformation = "مدیریت اطلاعات پایه";

        public const string Manager_BaseInformation_Tags = "مدیریت کلمات کلیدی";

    }
}
