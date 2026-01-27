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
        [JsonPropertyName("data")]
        public IEnumerable Data { get; set; } = Array.Empty<object>();

        [JsonPropertyName("totalCount")]
        [DefaultValue(-1)]
        public int TotalCount { get; set; } = -1;

        [JsonPropertyName("groupCount")]
        [DefaultValue(-1)]
        public int GroupCount { get; set; } = -1;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("summary")]
        public object[] Summary { get; set; } = [];
    }
}
