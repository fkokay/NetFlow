using NetFlow.Domain.Netsis.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Netsis.ExpenseAccountCodes
{
    public sealed class ExpenseAccountCode
    {
        public short BranchCode { get; }
        public string Code { get; }
        public string Name { get; }


        private ExpenseAccountCode(
           short branchCode,
           string code,
           string name)
        {
            BranchCode = branchCode;
            Code = code;
            Name = name;
        }

        public static ExpenseAccountCode Create(short branchCode, string code, string name)
        {
            return new ExpenseAccountCode(branchCode, code, name);
        }
    }
}
