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
            CreateMap<RecognitionTypes, RecognitionTypesDto>()
                .ForMember(
                dest => dest.RecognitionTypeName,
                opt => opt.MapFrom(src => $"{src.Name}"))
                .ForMember(
                dest => dest.RecognitionTypeCode,
                opt => opt.MapFrom(src => $"{src.Code}"));

            CreateMap<RecognitionTypes, RecognitionTypesViewPinsDto>()
                .ForMember(
                dest => dest.RecognitionTypeName,
                opt => opt.MapFrom(src => $"{src.Name}"))
                .ForMember(
                dest => dest.RecognitionTypeCode,
                opt => opt.MapFrom(src => $"{src.Code}"));

            CreateMap<RecognitionTypesDto, RecognitionTypes>()
                .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.RecognitionTypeName}"))
                .ForMember(
                dest => dest.Code,
                opt => opt.MapFrom(src => $"{src.RecognitionTypeCode}"));

        }
    }
}
