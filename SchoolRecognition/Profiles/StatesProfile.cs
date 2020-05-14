using AutoMapper;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class StatesProfile : Profile
    {
        public StatesProfile()
        {

            CreateMap<States, StatesViewDto>()
                .ForMember(
                dest => dest.StateName,
                opt => opt.MapFrom(src => $"{src.Name}"))
                .ForMember(
                dest => dest.StateCode,
                opt => opt.MapFrom(src => $"{src.Code}"));

            CreateMap<StatesCreateDto, States>()
                .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.StateName}"))
                .ForMember(
                dest => dest.Code,
                opt => opt.MapFrom(src => $"{src.StateCode}"));
        }
    }
}
