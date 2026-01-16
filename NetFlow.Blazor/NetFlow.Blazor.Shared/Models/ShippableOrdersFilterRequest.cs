using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Shared.Models
{
    public class ShippableOrdersFilterRequest
    {
        public string Customer { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Warehouse { get; set; }
        public bool HasBalance { get; set; }
    }
}
