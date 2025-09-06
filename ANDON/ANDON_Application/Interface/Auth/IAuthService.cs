using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANDON_Application.Auth;

namespace ANDON_Application.Interface.Auth
{
    public interface IAuthService
    {
        Task<UserDTO?> AuthenticationAsync(string username, string password);
    }
}
