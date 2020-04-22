using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class SchoolCategoryProfile : Profile
    {
        public SchoolCategoryProfile()
        {
            CreateMap<Entities.SchoolCategories, Models.SchoolCategoryDto>()
               .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.Name}"))
                .ForMember(
                    dest => dest.Code,
                    opt => opt.MapFrom(src => $"{src.Code}"));
        }
    }
}
