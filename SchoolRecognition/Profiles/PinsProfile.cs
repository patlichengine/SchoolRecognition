using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class PinsProfile:Profile
    {
        public PinsProfile()
        {

            CreateMap<Entities.Pins, Models.PinsDto>()
               .ForMember(
                    dest => dest.RecognitionTypeId,
                    opt => opt.MapFrom(src => $"{src.RecognitionTypeId}"))
             .ForMember(
                    dest => dest.SerialPin,
                    opt => opt.MapFrom(src => $"{src.SerialPin}"));






        }
    }
}
