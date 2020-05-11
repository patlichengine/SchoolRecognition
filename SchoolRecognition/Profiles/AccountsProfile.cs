using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class AccountsProfile : Profile
    {
        public AccountsProfile()
        {
            CreateMap<Entities.ApplicationUsers, Models.AccountsDto>()
                .ForMember(
                    dest => dest.Fullname,
                    opt => opt.MapFrom(src => $"{src.Surname } {src.Othernames}"));
        }
    }
}
