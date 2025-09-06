using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANDON_Domain.Entities
{
    public class User
    {
        public string UserName { get; set; }=string.Empty;
        public string Password { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
        public string TypeId { get; set; } = string.Empty;
        public string Password2 { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;   

    }
}
