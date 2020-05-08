using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class RolesProfile : Profile
    {
        public RolesProfile()
        {
            CreateMap<Entities.Roles, Models.RolesDto>()
                .ForMember(
                dest => dest.RoleName,
                opt => opt.MapFrom(src => $"{src.Name}")
                );
        }
    }
}
