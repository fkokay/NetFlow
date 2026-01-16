using NetFlow.Domain.Netsis.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Netsis.Products
{
    public sealed class Product
    {
        public short BranchCode { get; }
        public short BusinessCode { get; }
        public string Code { get; set; }
        public string Name { get; set; }

        public Product(short branchCode, short businessCode, string code, string name)
        {
            BranchCode = branchCode;
            BusinessCode = businessCode;
            Code = code;
            Name = name;
        }

        public static Product Create(short branchCode, short businessCode, string code, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidProductNameException();
            return new Product(
                branchCode,
                businessCode,
                code,
                name);
        }
    }
}
