using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class FacilitySettingsProfile : Profile
    {
      public  FacilitySettingsProfile()
        {
            CreateMap<Entities.FacilitySettings, Models.FacilitySettingsDto>()
                .ForMember(dest => dest.Subject,
            opt =>
            {
                opt.PreCondition(src => src.Subject != null);
                opt.MapFrom(src => src.Subject);
            }
            )
             .ForMember(dest => dest.SchoolFacilities,
             opt =>
             {
                 opt.PreCondition(src => (src.SchoolFacilities != null));
                 opt.MapFrom(src => src.SchoolFacilities);
             })
             .ForMember(dest => dest.FacilityType,
             opt =>
             {
                 opt.PreCondition(src => src.FacilityType != null);
                 opt.MapFrom(src => src.FacilityType);
             })
            .ForMember(dest => dest.FacilityItem,
             opt =>
             {
                 opt.PreCondition(src => src.FacilityItem != null);
                 opt.MapFrom(src => src.FacilityItem);
             })
             .ForMember(dest => dest.CreatedByNavigation,
             opt =>
             {
                 opt.PreCondition(src => (src.CreatedByNavigation != null));
                 opt.MapFrom(src => src.CreatedByNavigation);
             }

            );


            CreateMap<Models.CreateFacilitySettingsDto, Entities.FacilitySettings>().ReverseMap();
            CreateMap<Models.FacilityItemsUpdateDto, Entities.FacilitySettings>().ReverseMap();

           

        }
    }
}
