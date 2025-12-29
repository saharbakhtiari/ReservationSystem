namespace Domain.Contract.Enums
{
    public enum AdvanceSearchType
    {
        /// <summary>
        /// شامل هر یک از واژه ها باشد
        /// </summary>
        Normal = 0,
        /// <summary>
        /// نتیجه عبارت خواهد بود از مواردی که تمام کلمات   موجود باشند.
        /// </summary>
        AllWords = 1,
        /// <summary>
        /// نتیجه عبارت خواهد بود از مواردی که تمام کلمات عینا، با رعایت ترتیب  در آن موجود باشند.
        /// </summary>
        ExactContent = 2,
        /// <summary>
        ///  مواردی که حاوی این کلمات باشند از نتیجه ی جستجو حذف خواهند شد.
        /// </summary>
        ExceptWordsContent = 3
        
    }
}
