using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Common.Pagination
{
    public class PagedRequest
    {
        public string? Filter { get; set; }
        public string? Sort { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public bool? RequireTotalCount { get; set; }
        public bool? IsCountQuery { get; set; }
        public string? TotalSummary { get; set; }
        public string? Group { get; set; }
        public string? GroupSummary { get; set; }
        public bool RequireGroupCount { get; set; }
        public string? Select { get; set; }
    }
}
