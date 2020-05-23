using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class OfficeTypesProfile : Profile
    {
        public OfficeTypesProfile()
        {
            CreateMap<Entities.OfficeTypes, Models.OfficeTypesViewDto>()
            .ForMember(
                dest => dest.TypeDescription,
                opt => opt.MapFrom(src => $"{src.Description}"));


            CreateMap<Models.OfficeTypesCreateDto, Entities.OfficeTypes>()
            .ForMember(
                dest => dest.Description,
                opt => opt.MapFrom(src => $"{src.TypeDescription}"));



            CreateMap<Entities.OfficeTypes, Models.OfficeTypeViewPagedListOfficesDto>()
                .ForMember(
                dest => dest.TypeDescription,
                opt => opt.MapFrom(src => $"{src.Description}"));

            CreateMap<Models.OfficeTypesViewDto, Models.OfficeTypesCreateDto>()
                .ForMember(
                dest => dest.IsActive,
                opt => opt.NullSubstitute(false));
        }
    }
}
