using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Shared.Models
{
    public class CustomerModel
    {
        public short BranchCode { get; }
        public short BusinessCode { get; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
