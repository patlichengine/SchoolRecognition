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
                dest => dest.TotalPins,
                opt =>
                {
                    opt.PreCondition(src => (src.Pins != null));
                    opt.MapFrom(src => src.Pins.Count());
                })
                .ForMember(
                dest => dest.TotalActivePins,
                opt =>
                {
                    opt.PreCondition(src => (src.Pins != null));
                    opt.MapFrom(src => src.Pins.Count(x => x.IsActive == true));
                })
                .ForMember(
                dest => dest.TotalPins,
                opt =>
                {
                    opt.PreCondition(src => (src.Pins != null));
                    opt.MapFrom(src => src.Pins.Count(x => x.IsInUse == true));
                });

            CreateMap<RecognitionTypes, RecognitionTypesViewPagedListPinsDto>()
                .ForMember(
                dest => dest.RecognitionTypeName,
                opt => opt.MapFrom(src => $"{src.Name}"))
                .ForMember(
                dest => dest.RecognitionTypeCode,
                opt => opt.MapFrom(src => $"{src.Code}"))
                .ForMember(
                dest => dest.TotalPins,
                opt =>
                {
                    opt.PreCondition(src => (src.Pins != null));
                    opt.MapFrom(src => src.Pins.Count());
                })
                .ForMember(
                dest => dest.TotalActivePins,
                opt =>
                {
                    opt.PreCondition(src => (src.Pins != null));
                    opt.MapFrom(src => src.Pins.Count(x => x.IsActive == true));
                })
                .ForMember(
                dest => dest.TotalPins,
                opt =>
                {
                    opt.PreCondition(src => (src.Pins != null));
                    opt.MapFrom(src => src.Pins.Count(x => x.IsInUse == true));
                });

            CreateMap<RecognitionTypesCreateDto, RecognitionTypes>()
                .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.RecognitionTypeName}"))
                .ForMember(
                dest => dest.Code,
                opt => opt.MapFrom(src => $"{src.RecognitionTypeCode}"));

            CreateMap<RecognitionTypesViewDto, RecognitionTypesCreateDto>();

        }
    }
}
