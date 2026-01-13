using NetFlow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Identity.Exceptions
{
    public class InvalidEmailException : DomainException
    {
        public InvalidEmailException()
            : base("AUTH_02", "Email adresi boş olamaz") { }
    }
}
