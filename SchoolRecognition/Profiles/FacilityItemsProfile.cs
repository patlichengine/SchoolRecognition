using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class FacilityItemsProfile : Profile
    {
      public  FacilityItemsProfile()
        {
            CreateMap<Entities.FacilityItems, Models.FacilityItemsDto>()
                .ForMember(dest => dest.facilitySettings,
            opt =>
            {
                opt.PreCondition(src => src.FacilitySettings != null);
                opt.MapFrom(src => src.FacilitySettings);
            });


            CreateMap<Models.FacilityItemsCreateDto, Entities.FacilityItems>().ReverseMap();
            CreateMap<Models.FacilityItemsUpdateDto, Entities.FacilityItems>().ReverseMap();

           

        }
    }
}
