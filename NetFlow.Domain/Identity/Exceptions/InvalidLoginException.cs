using NetFlow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Identity.Exceptions
{
    public sealed class InvalidLoginException : DomainException
    {
        public InvalidLoginException() : base("AUTH_01", "Geçersiz kullanıcı veya şifre") { }

        public InvalidLoginException(string message) : base("AUTH_FIRM", message) { }
    }
}
