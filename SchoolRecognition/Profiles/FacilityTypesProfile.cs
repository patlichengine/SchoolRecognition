using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class FacilityTypesProfile : Profile
    {
      public  FacilityTypesProfile()
        {
            CreateMap<Entities.FacilityTypes, Models.FacilityTypesDto>().ReverseMap();
            CreateMap<Models.CreateFacilityTypesDto, Entities.FacilityTypes>().ReverseMap();
            CreateMap<Models.UpdateFacilityTypesDto, Entities.FacilityTypes>().ReverseMap();
        }
    }
}
