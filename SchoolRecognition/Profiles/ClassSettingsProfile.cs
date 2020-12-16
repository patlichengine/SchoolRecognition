using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class ClassSettingProfile : Profile
    {
      public  ClassSettingProfile()
        {
            CreateMap<Entities.ClassSettings, Models.ClassSettingsDto>()
                
              
               .ForMember(
                dest => dest.SchoolClasses,
                opt =>
                {
                    opt.PreCondition(src => (src.SchoolClasses != null));
                    opt.MapFrom(src => src.SchoolClasses);
                })
               .ForMember(
                dest => dest.SchoolStaffSubjects,
                opt =>
                {
                    opt.PreCondition(src => (src.SchoolStaffSubjects != null));
                    opt.MapFrom(src => src.SchoolStaffSubjects);
                })
                ;


            CreateMap<Models.ClassSettingsCreateDto, Entities.ClassSettings>().ReverseMap();
            CreateMap<Models.ClassSettingsUpdateDto, Entities.ClassSettings>().ReverseMap();

           

        }
    }
}
