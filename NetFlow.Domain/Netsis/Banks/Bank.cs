using NetFlow.Domain.Netsis.Customers;
using NetFlow.Domain.Netsis.ExpenseAccountCodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Netsis.Banks
{
    public class Bank
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public Bank(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public static Bank Create(
            string code,
            string name)
        {
            return new Bank(
                code,
                name);
        }
    }
}
