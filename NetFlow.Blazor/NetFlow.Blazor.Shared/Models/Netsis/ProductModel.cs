using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Shared.Models.Netsis
{
    public class ProductModel
    {
        public short BranchCode { get; }
        public short BusinessCode { get; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
