using NetFlow.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Auth
{
    public interface ITokenService
    {
        string CreateToken(User user);
        UserSnapshot? ReadSnapshot(string token);
    }
}
