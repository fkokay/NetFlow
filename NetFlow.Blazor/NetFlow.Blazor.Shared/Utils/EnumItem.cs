using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Shared.Utils
{
    public class EnumItem<TEnum>
    where TEnum : Enum
    {
        public TEnum Value { get; set; } = default!;
        public string Text { get; set; } = string.Empty;
    }
}
