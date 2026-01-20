using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace NetFlow.Application.Common.DevExtreme
{
    public sealed class DevExtremeSummaryInfo
    {
        [JsonPropertyName("selector")]
        public string Selector { get; set; } = default!;
        [JsonPropertyName("summaryType")]
        public string SummaryType { get; set; } = default!;
    }
}
