using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class ApplicationUser
    {
        public ApplicationUser()
        {
            ApplicationUserStates = new HashSet<ApplicationUserState>();
            AuditTrails = new HashSet<AuditTrail>();
            CentreSanctions = new HashSet<CentreSanction>();
            FacilitySettings = new HashSet<FacilitySetting>();
            LocationApprovedByNavigations = new HashSet<Location>();
            LocationCapturedByNavigations = new HashSet<Location>();
            PaymentPinSettings = new HashSet<PaymentPinSetting>();
            PaymentPins = new HashSet<PaymentPin>();
            PinHistories = new HashSet<PinHistory>();
            SchoolFacilities = new HashSet<SchoolFacility>();
            SchoolPayments = new HashSet<SchoolPayment>();
            SchoolStaffDegrees = new HashSet<SchoolStaffDegree>();
        }

        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string Othernames { get; set; }
        public byte[] Password { get; set; }
        public Guid? UserGroupId { get; set; }
        public int? RankId { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public string Lpno { get; set; }
        public byte[] Signature { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
        public bool IsLocked { get; set; }
        public long LoginCount { get; set; }
        public Guid? CountrySettingId { get; set; }

        public virtual CountrySetting CountrySetting { get; set; }
        public virtual OfficeRank Rank { get; set; }
        public virtual UserGroup UserGroup { get; set; }
        public virtual ICollection<ApplicationUserState> ApplicationUserStates { get; set; }
        public virtual ICollection<AuditTrail> AuditTrails { get; set; }
        public virtual ICollection<CentreSanction> CentreSanctions { get; set; }
        public virtual ICollection<FacilitySetting> FacilitySettings { get; set; }
        public virtual ICollection<Location> LocationApprovedByNavigations { get; set; }
        public virtual ICollection<Location> LocationCapturedByNavigations { get; set; }
        public virtual ICollection<PaymentPinSetting> PaymentPinSettings { get; set; }
        public virtual ICollection<PaymentPin> PaymentPins { get; set; }
        public virtual ICollection<PinHistory> PinHistories { get; set; }
        public virtual ICollection<SchoolFacility> SchoolFacilities { get; set; }
        public virtual ICollection<SchoolPayment> SchoolPayments { get; set; }
        public virtual ICollection<SchoolStaffDegree> SchoolStaffDegrees { get; set; }
    }
}
