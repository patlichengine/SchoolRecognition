using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class CentreProfile:Profile
    {
        public CentreProfile() 
        {
            CreateMap<Entities.Centres, Models.CentresDto>()
              .ForMember(
                   dest => dest.CentreNo,
                   opt => opt.MapFrom(src => $"{src.CentreNo}"))
            .ForMember(
                   dest => dest.CentreName,
                   opt => opt.MapFrom(src => $"{src.CentreName}"))
            .ForMember(
                   dest => dest.SchoolCategoryId,
                   opt => opt.MapFrom(src => $"{src.SchoolCategoryId}"));
        }
       
    }
}
