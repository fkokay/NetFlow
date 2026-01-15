using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Common.Pagination
{
    public class PagedRequest
    {
        public string? filter { get; set; }
        public string? sort { get; set; }
        public int? skip { get; set; }
        public int? take { get; set; }
        public bool? requireTotalCount { get; set; }
        public bool? isCountQuery { get; set; }
    }
}
