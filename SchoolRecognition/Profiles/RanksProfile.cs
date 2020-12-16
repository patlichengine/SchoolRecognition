using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class RanksProfile : Profile
    {
        public RanksProfile()
        {
            CreateMap<Entities.Ranks, Models.RanksDto>().ForMember(dest => dest.ApplicationUsers,
            opt =>
            {
                opt.PreCondition(src => src.ApplicationUsers != null);
                opt.MapFrom(src => src.ApplicationUsers);
            }
            );
            CreateMap<Models.CreateRanksDto, Entities.Ranks>().ReverseMap();
            CreateMap<Models.UpdateRanksDto, Entities.Ranks>().ReverseMap();


        }
    }
}
