using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class OfficesProfile : Profile
    {
        public OfficesProfile()
        {
            CreateMap<Entities.Offices, Models.OfficesDto>()
                .ForMember(
                    dest => dest.OfficeName,
                    opt => opt.MapFrom(src => $"{src.Name}"))
                .ForMember(
                    dest => dest.OfficeAddress,
                    opt => opt.MapFrom(src => $"{src.Address}"));
        }
    }
}
