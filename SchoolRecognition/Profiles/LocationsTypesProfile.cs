using AutoMapper;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;

namespace SchoolRecognition
{
    public class LocationsTypesProfile : Profile
    {
        public LocationsTypesProfile()
        {
            CreateMap<LocationTypes, LocationDto>().ReverseMap();
            CreateMap<CreateLocationDto, LocationTypes>().ReverseMap();
            CreateMap<UpdateLocationDto, LocationTypes>().ReverseMap();
        }
    }
}