using System.Collections.Generic;

namespace Application_Frontend.Common
{
    public class ProblemDetails
    {
        public string Detail { get; set; }
        public string Instance { get; set; }
        public int? Status { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
    }
}
