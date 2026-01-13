using NetFlow.Domain.Identity.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Identity
{
    public readonly struct FirmId
    {
        public int Id { get; }
        public string Code { get; }
        public FirmId(int id,string code)
        {
            if (id <= 0)
                throw new InvalidEmailException();

            Id = id;
            Code = code;
        }
    }
}
