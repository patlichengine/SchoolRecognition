using AutoMapper;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {

            CreateMap<Subjects, SubjectsViewDto>();

            CreateMap<SubjectsCreateDto, Subjects>();
        }
    }
}
