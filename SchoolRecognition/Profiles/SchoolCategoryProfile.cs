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
            .ForMember(dest => dest.Centres,
            opt =>
            {
                opt.PreCondition(src => src.Centres != null);
                opt.MapFrom(src => src.Centres);
            }
            )
             .ForMember(dest => dest.Schools,
             opt =>
             {
                 opt.PreCondition(src => (src.Schools != null));
                 opt.MapFrom(src => src.Schools);
             }
            );


            CreateMap<Models.CreateSchoolCategoryDto, Entities.SchoolCategories>().ReverseMap();
            CreateMap<Models.UpdateSchoolCategoryDto, Entities.SchoolCategories>().ReverseMap();


        }
    }
}
