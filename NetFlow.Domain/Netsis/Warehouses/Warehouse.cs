using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Netsis.Warehouses
{
    public sealed class Warehouse
    {
        public short BranchCode { get; }
        public short Code { get; }
        public string Name { get; }

        private Warehouse(
            short branchCode,
            short code,
            string name)
        {
            BranchCode = branchCode;
            Code = code;
            Name = name;
        }

        public static Warehouse Create(
            short branchCode,
            short code,
            string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidWarehouseNameException();
            return new Warehouse(
                branchCode,
                code,
                name);
        }
    }
}
