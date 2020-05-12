using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class ApplicationSettingsProfile : Profile
    {
        public ApplicationSettingsProfile()
        {

            CreateMap<Entities.ApplicationSettings, Models.ApplicationSettingsViewDto>();
            CreateMap<Models.ApplicationSettingsCreateDto, Entities.ApplicationSettings>();
        }
    }
}
