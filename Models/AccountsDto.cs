using SchoolRecognition.Entities;
using SchoolRecognition.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{


    public class AccountsDto
    {
        public Guid Id { get; set; }
        public string FullNames { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public string Lpno { get; set; }
        public byte[] Signature { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
        public  Ranks Rank { get; set; }
        public  ApplicationRoles Role { get; set; }
        public  IEnumerable<ApplicationUserStates> ApplicationUserStatesCreatedByNavigation { get; set; }
        public  IEnumerable<ApplicationUserStates> ApplicationUserStatesUser { get; set; }
        public  IEnumerable<AuditTrail> AuditTrail { get; set; }
        public  IEnumerable<CentreSanctions> CentreSanctions { get; set; }
        public  IEnumerable<Centres> Centres { get; set; }
        public  IEnumerable<FacilitySettings> FacilitySettings { get; set; }
        public  IEnumerable<Offices> Offices { get; set; }
        public  IEnumerable<PinHistories> PinHistories { get; set; }
        public  IEnumerable<Pins> Pins { get; set; }
        public  IEnumerable<SchoolFacilities> SchoolFacilities { get; set; }
        public  IEnumerable<SchoolPayments> SchoolPayments { get; set; }
        public  IEnumerable<SchoolStaffDegrees> SchoolStaffDegrees { get; set; }

        //ApplicationUserStatesCreatedByNavigation ApplicationUserStatesUser AuditTrail CentreSanctions Centres FacilitySettings 
        //Offices PinHistories Pins SchoolFacilities SchoolPayments SchoolStaffDegrees
    }

    public class AccountsCreateDto : AccountsManipulationDto
    {
        [Required]
        [MaxLength(150)]
        [EmailAddress(ErrorMessage = "Enter a valid Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }


    public class AccountsUpdateDto : AccountsManipulationDto //: IValidatableObject
    {
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if(Surname == OtherNames)
        //    {
        //        yield return new ValidationResult(
        //            "The provided Surname should be different from the Other Names",
        //            new[] { "AccountsCreateDto" }
        //            );
        //    }
        //}

        [Required(ErrorMessage = "The Phone number must be specified")]
        public override string PhoneNo { get => base.PhoneNo; set => base.PhoneNo = value; }
    }


    [SurnameMustBeDifferentAttribute(ErrorMessage = "The provided Surname should be different from the Other Names")]
    public abstract class AccountsManipulationDto
    {
        [Required(ErrorMessage = "The Surname must be specified and should not be more that 20 characters")]
        [MaxLength(20)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "The Other name must be specified and should not be more that 50 characters")]
        [MaxLength(50)]
        public string Othernames { get; set; }

        [MaxLength(14)]
        [RegularExpression(@"^\([0-9]{14})$", ErrorMessage = "Not a valid phone number")]
        public virtual string PhoneNo { get; set; }

    }
}
