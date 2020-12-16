using AutoMapper;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class SchoolClassesProfile : Profile
    {
        public SchoolClassesProfile()
        {
          
          CreateMap<SchoolClasses, SchoolClassesDto>()
                .ForMember(dest => dest.Class,
                    opt =>
                    {
                        opt.PreCondition(src => src.Class != null);
                        opt.MapFrom(src => src.Class);
                    })
                .ForMember(dest => dest.School, 
                opt => {
                    opt.PreCondition(src => src.School != null);
                    opt.MapFrom(src => src.School);
                }).ReverseMap();

            CreateMap<Models.CreateSchoolClassesDto, Entities.SchoolClasses>().ReverseMap();
            CreateMap<Models.UpdateSchoolClassesDto, Entities.SchoolClasses>().ReverseMap();

          
        }
    }
}
