using NetFlow.Domain.Netsis.Banks;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Netsis.BankBranches
{
    public class BankBranch
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public BankBranch(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public static BankBranch Create(
            string code,
            string name)
        {
            return new BankBranch(
                code,
                name);
        }
    }
}
