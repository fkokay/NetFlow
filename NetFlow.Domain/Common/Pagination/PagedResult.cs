using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;

namespace NetFlow.Domain.Common.Pagination
{
    public class PagedResult
    {
        public IEnumerable data { get; set; }

        [DefaultValue(-1)]
        public int totalCount { get; set; } = -1;

        [DefaultValue(-1)]
        public int groupCount { get; set; } = -1;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public object[] summary { get; set; }
    }
}
