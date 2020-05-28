using AutoMapper;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class SchoolPaymentsProfile : Profile
    {
        public SchoolPaymentsProfile()
        {
            CreateMap<SchoolPayments, SchoolPaymentsViewDto>()
                .ForMember(
                dest => dest.PaymentReceiptNo,
                opt => opt.MapFrom(src => $"{src.ReceiptNo}"))
                .ForMember(
                dest => dest.PaymentReceiptImage,
                opt => opt.MapFrom(src => $"{src.ReceiptImage}"))
                .ForMember(
                dest => dest.SchoolName,
                opt =>
                {
                    opt.PreCondition(src => (src.School != null));
                    opt.MapFrom(src => $"{ src.School.Name}");
                })
                .ForMember(
                dest => dest.SchoolCategoryName,
                opt =>
                {
                    opt.PreCondition(src => (src.School != null && src.School.Category != null));
                    opt.MapFrom(src => $"{ src.School.Category.Name}");
                })
                .ForMember(
                dest => dest.PinSerialNumber,
                opt =>
                {
                    opt.PreCondition(src => (src.Pin != null));
                    opt.MapFrom(src => $"{ src.Pin.SerialPin}");
                })
                .ForMember(
                dest => dest.RecognitionTypeName,
                opt =>
                {
                    opt.PreCondition(src => (src.Pin != null && src.Pin.RecognitionType != null));
                    opt.MapFrom(src => $"{ src.Pin.RecognitionType.Name}");
                })
                .ForMember(
                dest => dest.RecognitionTypeCode,
                opt =>
                {
                    opt.PreCondition(src => (src.Pin != null && src.Pin.RecognitionType != null));
                    opt.MapFrom(src => $"{ src.Pin.RecognitionType.Code}");
                });
        }
    }
}
