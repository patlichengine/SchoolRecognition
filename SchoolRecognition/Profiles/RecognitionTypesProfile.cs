using AutoMapper;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class RecognitionTypesProfile : Profile
    {
        public RecognitionTypesProfile()
        {
            CreateMap<RecognitionTypes, RecognitionTypesViewDto>()
                .ForMember(
                dest => dest.RecognitionTypeName,
                opt => opt.MapFrom(src => $"{src.Name}"))
                .ForMember(
                dest => dest.RecognitionTypeCode,
                opt => opt.MapFrom(src => $"{src.Code}"))
                .ForMember(
                dest => dest.RecognitionTypePins,
                opt =>
                {
                    opt.PreCondition(src => (src.Pins.Count() > 0));
                    opt.MapFrom(src => $"{ src.Pins}");
                });

            CreateMap<RecognitionTypes, RecognitionTypeViewPagedListPinsDto>()
                .ForMember(
                dest => dest.RecognitionTypeName,
                opt => opt.MapFrom(src => $"{src.Name}"))
                .ForMember(
                dest => dest.RecognitionTypeCode,
                opt => opt.MapFrom(src => $"{src.Code}"));

            CreateMap<RecognitionTypesCreateDto, RecognitionTypes>()
                .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.RecognitionTypeName}"))
                .ForMember(
                dest => dest.Code,
                opt => opt.MapFrom(src => $"{src.RecognitionTypeCode}"));

        }
    }
}
