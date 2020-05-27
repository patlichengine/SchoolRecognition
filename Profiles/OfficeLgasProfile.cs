using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class OfficeLgasProfile : Profile
    {
        public OfficeLgasProfile()
        {
            CreateMap<Entities.OfficeLgas, Models.OfficeLgasDto>()
                  .ForMember(
                       dest => dest.OfficeID,
                       opt => opt.MapFrom(src => $"{src.OfficeID}"))
                .ForMember(
                       dest => dest.LgaID,
                       opt => opt.MapFrom(src => $"{src.LgaID}"));


        }
    }
}

