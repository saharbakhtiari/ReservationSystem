using System;

namespace Application.AdvanceSearch.Queries.SearchRuleVersion
{
    public class SearchRuleVersionDto
    {
        public long Id { get; set; }
        
        public long RuleId { get; set; }

        public string RuleTypeTitle { get; set; }
        /// <summary>
        /// شماره ورژن
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// عنوان
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// توضیح
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// شماره مصوبه
        /// </summary>
        public string ApprovalNo { get; set; }

        /// <summary>
        /// تاریخ تصویب
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// نهاد تصویب کننده
        /// </summary>
        public string ApprovalInstitution { get; set; }

        /// <summary>
        /// شماره نامه انتشار
        /// </summary>
        public string PublishLetterNo { get; set; }

        /// <summary>
        /// تاریخ انتشار
        /// </summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// وضعیت تنقیحی
        /// </summary>
        public string TanghihStatus { get; set; }

        /// <summary>
        /// تاریخ اجرا
        /// </summary>
        public DateTime? ExecuteDate { get; set; }

        /// <summary>
        /// شماره اجرا
        /// </summary>
        public string ExecuteNo { get; set; }

        /// <summary>
        /// مرجع ابلاغ
        /// </summary>
        public string ExecuteAuthority { get; set; }

        /// <summary>
        /// شماره ابلاغ
        /// </summary>
        public string NotificationNo { get; set; }

        /// <summary>
        /// تاریخ ابلاغ
        /// </summary>
        public DateTime? NotificationDate { get; set; }

        /// <summary>
        /// مرجع ابلاغ
        /// </summary>
        public string NotificationAuthority { get; set; }
        public string CategoryTitle { get; set; }
    }

    public class SearchRuleVersionFileDto
    {
      
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
    }

}
