using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class OfficeLocalGovernmentsProfile : Profile
    {
        public OfficeLocalGovernmentsProfile()
        {

            CreateMap<Entities.OfficeLocalGovernments, Models.OfficeLocalGovernmentsViewDto>()
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
                dest => dest.OfficeTypeDescription,
                opt =>
                {
                    opt.PreCondition(src => (src.Office != null && src.Office.OfficeType != null));
                    opt.MapFrom(src => $"{ src.Office.OfficeType.Description}");
                })
            .ForMember(
                dest => dest.LocalGovernmentName,
                opt =>
                {
                    opt.PreCondition(src => (src.LocalGovernment != null));
                    opt.MapFrom(src => $"{ src.LocalGovernment.Name}");
                })
            .ForMember(
                dest => dest.LocalGovernmentCode,
                opt =>
                {
                    opt.PreCondition(src => (src.LocalGovernment != null));
                    opt.MapFrom(src => $"{ src.LocalGovernment.Code}");
                })            
            .ForMember(
                dest => dest.StateName,
                opt =>
                {
                    opt.PreCondition(src => (src.LocalGovernment != null & src.LocalGovernment.State != null));
                    opt.MapFrom(src => $"{ src.LocalGovernment.State.Name}");
                })
            .ForMember(
                dest => dest.StateCode,
                opt =>
                {
                    opt.PreCondition(src => (src.LocalGovernment != null & src.LocalGovernment.State != null));
                    opt.MapFrom(src => $"{ src.LocalGovernment.State.Code}");
                });


            CreateMap<Models.OfficeLocalGovernmentsCreateDto, Entities.OfficeLocalGovernments>();



        }
    }
}
