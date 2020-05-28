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
            CreateMap<Entities.Schools, Models.SchoolsViewDto>()
               .ForMember(
                dest => dest.SchoolName,
                opt => opt.MapFrom(src => $"{src.Name}"))
               //SchoolCategory
               .ForMember(
                dest => dest.SchoolCategoryName,
                opt =>
                {
                    opt.PreCondition(src => (src.Category != null));
                    opt.MapFrom(src => $"{src.Category.Name}");
                })
               .ForMember(
                dest => dest.SchoolCategoryCode,
                opt =>
                {
                    opt.PreCondition(src => (src.Category != null));
                    opt.MapFrom(src => $"{src.Category.Code}");
                })
               //Office
               .ForMember(
                dest => dest.OfficeName,
                opt =>
                {
                    opt.PreCondition(src => (src.Office != null));
                    opt.MapFrom(src => $"{src.Office.Name}");
                })
               //LGA
               .ForMember(
                dest => dest.LgaName,
                opt =>
                {
                    opt.PreCondition(src => (src.Lg != null));
                    opt.MapFrom(src => $"{src.Lg.Name}");
                })
               .ForMember(
                dest => dest.LgaCode,
                opt =>
                {
                    opt.PreCondition(src => (src.Lg != null));
                    opt.MapFrom(src => $"{src.Lg.Code}");
                })
               //State
               .ForMember(
                dest => dest.StateName,
                opt =>
                {
                    opt.PreCondition(src => (src.Lg != null && src.Lg.StateId != null));
                    opt.MapFrom(src => $"{src.Lg.State.Name}");
                })
               .ForMember(
                dest => dest.StateCode,
                opt =>
                {
                    opt.PreCondition(src => (src.Lg != null && src.Lg.StateId != null));
                    opt.MapFrom(src => $"{src.Lg.State.Code}");
                });




            CreateMap<Models.SchoolsCreateDto, Entities.Schools>()
               .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.SchoolName}"));

        }
    }
}
