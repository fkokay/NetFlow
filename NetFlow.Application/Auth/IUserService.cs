using NetFlow.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Auth
{
    public interface IUserService
    {
        /// <summary>
        /// Kullanıcı adı + şifre ile giriş yapar.
        /// Başarılıysa tam yetkili Domain User döner.
        /// </summary>
        Task<User> Authenticate(string email, string password,string firmCode);

        /// <summary>
        /// Token içinden gelen user id ile kullanıcıyı tekrar yükler.
        /// (Permission ve Firm ile birlikte)
        /// </summary>
        Task<User> GetById(int id,string firmCode);
    }
}
