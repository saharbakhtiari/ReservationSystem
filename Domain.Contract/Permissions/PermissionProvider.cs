using System.Collections.Generic;
using System.Linq;

namespace Domain.Permissions
{
    public class PermissionProvider
    {
        public record Permission(string Code, string Title, string ParentCode = null)
        {
            public int Level => Parent?.Level + 1 ?? 0;

            public Permission Parent =>
                ParentCode is null
                    ? null
                    : Permissions.First(x => x.Code == ParentCode);

            public IEnumerable<Permission> Children =>
                Permissions.Where(x => x.ParentCode == Code);
        }

        public static readonly List<Permission> Permissions = new()
        {
            new (PermissionNames.Manager, "مدیر اطلاعات"),

            new (PermissionNames.Manager_UserManager, "مدیریت کاربران", PermissionNames.Manager),
            new (PermissionNames.Manager_DepartmentManager, "مدیریت ساختار سازمانی", PermissionNames.Manager),
            new (PermissionNames.Manager_RoleManager, "مدیریت نقش ها", PermissionNames.Manager),

            new (PermissionNames.Manager_DepartmentPublishment, "انتشار ساختار سازمانی", PermissionNames.Manager),
            new (PermissionNames.Manager_DigitalSolutionManager, "مدیریت راهکارهای دیجیتال", PermissionNames.Manager),
            new (PermissionNames.Manager_DigitalSolutionPublishment, "انتشار راهکارهای دیجیتال", PermissionNames.Manager),

            new (PermissionNames.Manager_ServiceManager, "مدیریت خدمت", PermissionNames.Manager),
            new (PermissionNames.Manager_ServicePublishment, "انتشار خدمت", PermissionNames.Manager),
            new (PermissionNames.Manager_ServiceClientManager, "مدیریت گیرنده خدمت", PermissionNames.Manager),

            new (PermissionNames.Manager_EducationManager, "مدیریت آموزش و فرهنگ سازی", PermissionNames.Manager),
            new (PermissionNames.Manager_EducationPublishment, "انتشار آموزش و فرهنگ سازی", PermissionNames.Manager),

            new (PermissionNames.Manager_StatisticalReportManager, "مدیریت گزارشهای آماری", PermissionNames.Manager),
            new (PermissionNames.Manager_StatisticalReportPublishment, "انتشار گزارشهای آماری", PermissionNames.Manager),

            new (PermissionNames.Manager_OffenseManager, "مدیریت تخلفات و جرائم", PermissionNames.Manager),
            new (PermissionNames.Manager_PageManager, "مدیریت صفحات", PermissionNames.Manager),
            new (PermissionNames.Manager_AnnouncementManager, "مدیریت اطلاعیه ها", PermissionNames.Manager),

            new (PermissionNames.Manager_NewsManager, "مدیریت اخبار", PermissionNames.Manager),
            new (PermissionNames.Manager_NewsPublishment, "انتشار اخبار", PermissionNames.Manager),

            new (PermissionNames.Manager_HomepageCardManager, "مدیریت صفحه اصلی", PermissionNames.Manager),
            new (PermissionNames.Manager_HomepageCardPublishment, "انتشار صفحه اصلی", PermissionNames.Manager),

            new (PermissionNames.Manager_SliderManager, "مدیریت اسلایدر", PermissionNames.Manager),
            new (PermissionNames.Manager_SliderPublishment, "انتشار اسلایدر", PermissionNames.Manager),

            new (PermissionNames.Manager_RuleSurveyView, "مشاهده نظرسنجی ها"),
            new (PermissionNames.Manager_RuleSurvey_Comment_Write, "شرکت در نظرسنجی"),

            new (PermissionNames.Manager_BaseInformation, "مدیریت اطلاعات پایه", PermissionNames.Manager),
            new (PermissionNames.Manager_File_Manager, "مدیریت فایل", PermissionNames.Manager),
            new (PermissionNames.Manager_InvitationManager, "مدیریت فراخوان", PermissionNames.Manager),
            new (PermissionNames.Manager_InvitationView, "مشاهده فراخوان", PermissionNames.Manager),
        };
    }

    public static class PermissionNames
    {
        public const string Manager = "MANAGER";

        public const string Manager_UserManager = "USER_MANAGER";
        public const string Manager_RoleManager = "ROLE_MANAGER";

        public const string Manager_DepartmentManager = "DEPARTMENT_MANAGER";
        public const string Manager_DepartmentPublishment = "DEPARTMENT_PUBLISH";

        public const string Manager_DigitalSolutionManager = "DIGITAL_SOLUTION_MANAGER";
        public const string Manager_DigitalSolutionPublishment = "DIGITAL_SOLUTION_PUBLISH";

        public const string Manager_ServiceManager = "SERVICE_MANAGER";
        public const string Manager_ServicePublishment = "SERVICE_PUBLISH";
        public const string Manager_ServiceClientManager = "SERVICE_CLIENT_MANAGER";

        public const string Manager_EducationManager = "EDUCATION_MANAGER";
        public const string Manager_EducationPublishment = "EDUCATION_PUBLISH";

        public const string Manager_StatisticalReportManager = "STAT_REPORT_MANAGER";
        public const string Manager_StatisticalReportPublishment = "STAT_REPORT_PUBLISH";

        public const string Manager_OffenseManager = "OFFENSE_MANAGER";
        public const string Manager_PageManager = "PAGE_MANAGER";
        public const string Manager_AnnouncementManager = "ANNOUNCEMENT_MANAGER";

        public const string Manager_NewsManager = "NEWS_MANAGER";
        public const string Manager_NewsPublishment = "NEWS_PUBLISH";

        public const string Manager_HomepageCardManager = "HOMEPAGE_CARD_MANAGER";
        public const string Manager_HomepageCardPublishment = "HOMEPAGE_CARD_PUBLISH";

        public const string Manager_SliderManager = "SLIDER_MANAGER";
        public const string Manager_SliderPublishment = "SLIDER_PUBLISH";

        public const string Manager_RuleSurveyView = "RULE_SURVEY_VIEW";
        public const string Manager_RuleSurvey_Comment_Write = "RULE_SURVEY_COMMENT";

        public const string Manager_BaseInformation = "BASE_INFORMATION";
        public const string Manager_File_Manager = "File_MANAGER";
        public const string Manager_InvitationManager = "INVITATION_MANAGER";
        public const string Manager_InvitationView = "INVITATION_MANAGER_VIEW";
    }
}
