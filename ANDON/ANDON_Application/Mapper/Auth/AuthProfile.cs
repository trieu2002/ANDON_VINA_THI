using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANDON_Application.Auth;
using ANDON_Domain.Entities;
using AutoMapper;

namespace ANDON_Application.Mapper.Auth
{
    public class AuthProfile:Profile
    {
        public AuthProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
