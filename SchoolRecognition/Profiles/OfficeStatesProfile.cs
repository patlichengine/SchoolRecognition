using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class OfficeStatesProfile : Profile
    {
        public OfficeStatesProfile()
        {

            CreateMap<Entities.OfficeStates, Models.OfficeStatesViewDto>()
            .ForMember(
                dest => dest.OfficeName,
                opt =>
                {
                    opt.PreCondition(src => (src.Office != null));
                    opt.MapFrom(src => $"{ src.Office.Name}");
                })
            .ForMember(
                dest => dest.OfficeAddress,
                opt =>
                {
                    opt.PreCondition(src => (src.Office != null));
                    opt.MapFrom(src => $"{ src.Office.Address}");
                })
            .ForMember(
                dest => dest.StateName,
                opt =>
                {
                    opt.PreCondition(src => (src.State != null));
                    opt.MapFrom(src => $"{ src.State.Name}");
                });


            CreateMap<Models.OfficeStatesCreateDto, Entities.OfficeStates>();



        }
    }
}
