using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANDON_Application.DTO;
using ANDON_Domain.Entities;
using ANDON_Domain.Model;
using AutoMapper;

namespace ANDON_Application.Mapper.Routes
{
    public class RouteProfile: Profile
    {
        public RouteProfile()
        {
            CreateMap<RouteError, RouteErrorDTO>().ReverseMap();
            CreateMap<Route, RouteDTO>().ReverseMap();
        }
    }
}
