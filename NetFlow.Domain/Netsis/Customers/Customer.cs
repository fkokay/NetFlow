using NetFlow.Domain.Netsis.Warehouses;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Netsis.Customers
{
    public sealed class Customer
    {
        public short BranchCode { get; }
        public short BusinessCode { get; }
        public string Code { get; set; }
        public string Name { get; set; }

        public Customer(short branchCode,short businessCode,string code,string name) 
        {
            BranchCode = branchCode;
            BusinessCode = businessCode;
            Code = code;
            Name = name;
        }

        public static Customer Create(short branchCode, short businessCode, string code, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidCustomerNameException();
            return new Customer(
                branchCode,
                businessCode,
                code,
                name);
        }


    }
}
