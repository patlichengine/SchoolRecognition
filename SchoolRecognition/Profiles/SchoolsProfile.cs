using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class SchoolsProfile : Profile
    {
        public SchoolsProfile()
        {
            CreateMap<Entities.Schools, Models.SchoolsDto>()
               .ForMember( dest => dest.Name,opt => opt.MapFrom(src => $"{src.Name}"))
                 .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => $"{src.Category.Id}"))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => $"{src.Category.Name}"))
                  .ForMember(dest => dest.LgId, opt => opt.MapFrom(src => $"{src.Lg.Id}"))
                  .ForMember(dest => dest.LgName, opt => opt.MapFrom(src => $"{src.Lg.Name}"))
                   .ForMember(dest => dest.OfficeId, opt => opt.MapFrom(src => $"{src.Office.Id}"))
                  .ForMember(dest => dest.OfficeName, opt => opt.MapFrom(src => $"{src.Office.Name}"))
                .ForMember(dest => dest.PhoneNo, opt => opt.MapFrom(src => $"{src.PhoneNo}"))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address}"));

            CreateMap<Models.CreateSchoolsDto, Entities.Schools>();
            CreateMap<Models.UpdateSchoolsDto, Entities.Schools>();


            CreateMap<Entities.Offices, Models.OfficesViewDto>();
            CreateMap<Entities.SchoolCategories, Models.SchoolCategoryDto>();
            CreateMap<Entities.LocalGovernments, Models.LocalGovernmentsDto>();

            CreateMap<Models.SchoolsDto, Entities.Schools>();

            CreateMap<Models.OfficesViewDto, Entities.Offices>();
            CreateMap<Models.SchoolCategoryDto, Entities.SchoolCategories>();
            CreateMap<Models.LocalGovernmentsDto, Entities.LocalGovernments>();

        }
    }
}
