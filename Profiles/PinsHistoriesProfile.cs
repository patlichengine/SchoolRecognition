using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class PinsHistoriesProfile:Profile
    {
        public PinsHistoriesProfile()
        {
            CreateMap<Entities.PinHistories, Models.PinHistoriesDto>()
                  .ForMember(
                       dest => dest.PinId,
                       opt => opt.MapFrom(src => src.PinId))
                .ForMember(
                       dest => dest.SchoolId,
                       opt => opt.MapFrom(src => $"{src.SchoolId}"));

            CreateMap<Entities.PinHistories, Models.PinHistoriesCreateDto>().ReverseMap();
        }
        
    }
}
