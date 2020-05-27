using AutoMapper;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class PinsProfile : Profile
    {
        public PinsProfile()
        {
            CreateMap<Pins, PinsViewDto>()
                .ForMember(
                dest => dest.SerialNumber,
                opt => opt.MapFrom(src => $"{src.SerialPin}"))
                .ForMember(
                dest => dest.RecognitionTypeName,
                opt => {
                    opt.PreCondition(src => (src.RecognitionType != null));
                    opt.MapFrom(src => $"{ src.RecognitionType.Name}");
                })
                .ForMember(
                dest => dest.CreatedByUser,
                opt => {
                    opt.PreCondition(src => (src.CreatedByNavigation != null));
                    opt.MapFrom(src => $"{src.CreatedByNavigation.Surname} {src.CreatedByNavigation.Othernames}");
                });

            CreateMap<Pins, PinsViewPagedListPinHistoriesDto>()
                .ForMember(
                dest => dest.SerialNumber,
                opt => opt.MapFrom(src => $"{src.SerialPin}"))
                .ForMember(
                dest => dest.RecognitionTypeName,
                opt => {
                    opt.PreCondition(src => (src.RecognitionType != null));
                    opt.MapFrom(src => $"{ src.RecognitionType.Name}");
                })
                .ForMember(
                dest => dest.CreatedByUser,
                opt => {
                    opt.PreCondition(src => (src.CreatedByNavigation != null));
                    opt.MapFrom(src => $"{src.CreatedByNavigation.Surname} {src.CreatedByNavigation.Othernames}");
                });

            CreateMap<PinsUpdateDto, Pins>()
                .ForMember(
                dest => dest.SerialPin,
                opt => opt.MapFrom(src => $"{src.SerialNumber}"));
        }
    }
}
