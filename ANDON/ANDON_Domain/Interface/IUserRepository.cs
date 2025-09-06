using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANDON_Domain.Entities;

namespace ANDON_Domain.Interface
{
    public interface IUserRepository
    {
        /// <summary>
        /// Thực hiện đăng nhập
        /// </summary>
        /// <param name="username">Nhập của tên Celline</param>
        /// <param name="password">Nhập mật khẩu</param>
        /// <returns>Trẩ về Usr của người dùng</returns>
        Task<User?> AuthenticationAsync(string username, string password);
    }
}
