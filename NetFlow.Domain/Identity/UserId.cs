using NetFlow.Domain.Identity.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Identity
{
    public readonly struct UserId
    {
        public int Value { get; }

        public UserId(int value)
        {
            if (value <= 0)
                throw new InvalidEmailException();

            Value = value;
        }
    }
}
