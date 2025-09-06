using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANDON_Application.DTO;
using ANDON_Domain.Entities;
using AutoMapper;

namespace ANDON_Application.Mapper
{
    public class DefectProfile: Profile

    {
        public DefectProfile() 
        {
            CreateMap<Defect,DefectDTO>().ReverseMap();
        }
    }
}
