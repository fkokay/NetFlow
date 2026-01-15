using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Common.Pagination
{
    public class PagedRequest
    {
        public string? Filter { get; set; }
        public string? Sort { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public bool? RequireTotalCount { get; set; }
        public bool? IsCountQuery { get; set; }
    }
}
