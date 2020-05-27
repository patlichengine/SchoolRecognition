using AutoMapper;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Profiles
{
    public class UsersProfile:Profile
    {
        public UsersProfile()
        {
            CreateMap<Users, UserDto>();
            CreateMap<UserDto, Users>();
        }
    }
}
