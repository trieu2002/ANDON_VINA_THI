using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANDON_Domain.Interface
{
    public interface IJwtRepository
    {
        public string GenerateToken(string username, string password,string groupId);
    }
}
