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
            CreateMap<Entities.Offices, Models.OfficesViewDto>()
                .ForMember(
                    dest => dest.OfficeName,
                    opt => opt.MapFrom(src => $"{src.Name}"))
                .ForMember(
                    dest => dest.OfficeAddress,
                    opt => opt.MapFrom(src => $"{src.Address}"))
                .ForMember(
                    dest => dest.StateName,
                    opt =>
                    {
                        opt.PreCondition(src => (src.State != null));
                        opt.MapFrom(src => $"{ src.State.Name}");
                    })
                .ForMember(
                    dest => dest.OfficeTypeDescription,
                    opt =>
                    {
                        opt.PreCondition(src => (src.OfficeType != null));
                        opt.MapFrom(src => $"{ src.OfficeType.Description}");
                    })
                .ForMember(
                    dest => dest.CreatedByUser,
                    opt =>
                    {
                        opt.PreCondition(src => (src.CreatedByNavigation != null));
                        opt.MapFrom(src => $"{ src.CreatedByNavigation.Surname}, {src.CreatedByNavigation.Othernames}");
                    });


            CreateMap<Models.OfficesCreateDto, Entities.Offices>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.OfficeName}"))
                .ForMember(
                    dest => dest.Address,
                    opt => opt.MapFrom(src => $"{src.OfficeAddress}"));



        }
    }
}
