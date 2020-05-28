using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class CentreProfile:Profile
    {
        public CentreProfile() 
        {
            CreateMap<Entities.Centres, Models.CentresViewDto>()
              .ForMember(
                   dest => dest.CentreNo,
                   opt => opt.MapFrom(src => $"{src.CentreNo}"))
            .ForMember(
                   dest => dest.CentreName,
                   opt => opt.MapFrom(src => $"{src.CentreName}"))
               //SchoolCategory
               .ForMember(
                dest => dest.SchoolCategoryName,
                opt =>
                {
                    opt.PreCondition(src => (src.SchoolCategory != null));
                    opt.MapFrom(src => $"{src.SchoolCategory.Name}");
                })
               .ForMember(
                dest => dest.SchoolCategoryCode,
                opt =>
                {
                    opt.PreCondition(src => (src.SchoolCategory != null));
                    opt.MapFrom(src => $"{src.SchoolCategory.Code}");
                })
               //CreatedBy
               .ForMember(
                dest => dest.CreatedByUser,
                opt =>
                {
                    opt.PreCondition(src => (src.CreatedByNavigation != null));
                    opt.MapFrom(src => $"{src.CreatedByNavigation.Surname} {src.CreatedByNavigation.Othernames}");
                });
               //
        }
       
    }
}
