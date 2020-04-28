using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class RecognitionTypeProfile:Profile
    {
        public RecognitionTypeProfile()
        {
            CreateMap<Entities.RecognitionTypes, Models.RecognitionTypesDto>()
              .ForMember(
                   dest => dest.Name,
                   opt => opt.MapFrom(src => $"{src.Name}"))
               .ForMember(
                   dest => dest.Code,
                   opt => opt.MapFrom(src => $"{src.Code}"));
        }
           
    }
}
