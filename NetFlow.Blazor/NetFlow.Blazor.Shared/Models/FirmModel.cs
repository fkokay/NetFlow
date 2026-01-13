using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Shared.Models
{
    public class FirmModel
    {
        public int Id { get; set; }
        public string FirmName { get; set; } = string.Empty;
        public string FirmCode { get; set; } = string.Empty;
    }
}
