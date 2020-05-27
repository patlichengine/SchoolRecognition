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
            CreateMap<Entities.ApplicationRoles, Models.ApplicationRolesDto>()
                .ForMember(
                dest => dest.RoleName,
                opt => opt.MapFrom(src => $"{src.Name}")
                );
        }
    }
}
