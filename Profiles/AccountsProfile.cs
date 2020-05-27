using AutoMapper;
using SchoolRecognition.Classes;
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
           // ApplicationUserStatesCreatedByNavigation ApplicationUserStatesUser AuditTrail CentreSanctions Centres FacilitySettings
        //Offices PinHistories Pins SchoolFacilities SchoolPayments SchoolStaffDegrees
            CreateMap<Entities.ApplicationUsers, Models.AccountsDto>()
                .ForMember(
                    dest => dest.FullNames,
                    opt => opt.MapFrom(src => $"{src.Surname } {src.Othernames}"))
                .ForMember(
                    dest => dest.Role,
                    opt =>
                    {
                        opt.PreCondition(src => (src.Role != null));
                        opt.MapFrom(src => src.Role);
                    }
                )
                .ForMember(
                    dest => dest.Rank,
                    opt =>
                    {
                        opt.PreCondition(src => (src.Rank != null));
                        opt.MapFrom(src => src.Rank);
                    }
                )
                .ForMember(
                    dest => dest.ApplicationUserStatesCreatedByNavigation,
                    opt =>
                    {
                        opt.PreCondition(src => (src.ApplicationUserStatesCreatedByNavigation != null));
                        opt.MapFrom(src => src.ApplicationUserStatesCreatedByNavigation);
                    }
                )
                .ForMember(
                    dest => dest.ApplicationUserStatesUser,
                    opt =>
                    {
                        opt.PreCondition(src => (src.ApplicationUserStatesUser != null));
                        opt.MapFrom(src => src.ApplicationUserStatesUser);
                    }
                )
                .ForMember(
                    dest => dest.AuditTrail,
                    opt =>
                    {
                        opt.PreCondition(src => (src.AuditTrail != null));
                        opt.MapFrom(src => src.AuditTrail);
                    }
                )
                .ForMember(
                    dest => dest.CentreSanctions,
                    opt =>
                    {
                        opt.PreCondition(src => (src.CentreSanctions != null));
                        opt.MapFrom(src => src.CentreSanctions);
                    }
                )
                .ForMember(
                    dest => dest.Centres,
                    opt =>
                    {
                        opt.PreCondition(src => (src.Centres != null));
                        opt.MapFrom(src => src.Centres);
                    }
                )
                .ForMember(
                    dest => dest.FacilitySettings,
                    opt =>
                    {
                        opt.PreCondition(src => (src.FacilitySettings != null));
                        opt.MapFrom(src => src.FacilitySettings);
                    }
                )
                .ForMember(
                    dest => dest.Offices,
                    opt =>
                    {
                        opt.PreCondition(src => (src.Offices != null));
                        opt.MapFrom(src => src.Offices);
                    }
                )
                .ForMember(
                    dest => dest.PinHistories,
                    opt =>
                    {
                        opt.PreCondition(src => (src.PinHistories != null));
                        opt.MapFrom(src => src.PinHistories);
                    }
                )
                .ForMember(
                    dest => dest.Pins,
                    opt =>
                    {
                        opt.PreCondition(src => (src.Pins != null));
                        opt.MapFrom(src => src.Pins);
                    }
                )
                .ForMember(
                    dest => dest.SchoolFacilities,
                    opt =>
                    {
                        opt.PreCondition(src => (src.SchoolFacilities != null));
                        opt.MapFrom(src => src.SchoolFacilities);
                    }
                )
                .ForMember(
                    dest => dest.SchoolPayments,
                    opt =>
                    {
                        opt.PreCondition(src => (src.SchoolPayments != null));
                        opt.MapFrom(src => src.SchoolPayments);
                    }
                )
                .ForMember(
                    dest => dest.SchoolStaffDegrees,
                    opt =>
                    {
                        opt.PreCondition(src => (src.SchoolStaffDegrees != null));
                        opt.MapFrom(src => src.SchoolStaffDegrees);
                    }
                );

            CreateMap<Entities.ApplicationRoles, Models.RolesDto>()
                .ForMember(
                dest => dest.RoleName,
                opt => opt.MapFrom(src => $"{src.Name}"));

            CreateMap<Models.AccountsCreateDto, Entities.ApplicationUsers>()
                .ForMember(
                    dest => dest.Password,
                    opt => opt.MapFrom(src => Encryption.EncryptPassword(src.Password))
                    );
            CreateMap<Models.AccountsUpdateDto, Entities.ApplicationUsers>();
        }
    }
}
