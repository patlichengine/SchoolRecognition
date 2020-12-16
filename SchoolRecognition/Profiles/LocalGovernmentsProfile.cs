using AutoMapper;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;

namespace SchoolRecognition
{
    public class LocalGovernmentsProfile : Profile
    {
        public LocalGovernmentsProfile()
        {
            CreateMap<LocalGovernments, LocalGovernmentsDto>()
                .ForMember(dest => dest.State,
                opt =>
                {
                    opt.PreCondition(src => src.State != null);
                    opt.MapFrom(src => src.State);
                })
                .ForMember(dest => dest.Schools,
                opt => {

                    opt.PreCondition(src => src.Schools != null);
                    opt.MapFrom(src => src.Schools);

                });
            CreateMap<CreateLocalGovernmentDto, LocalGovernments>().ReverseMap();
            CreateMap<UpdateLocalGovernmentsDto, LocalGovernments>().ReverseMap();
        }
    }
}