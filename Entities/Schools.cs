using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class Schools
    {
        public Schools()
        {
            PinHistories = new HashSet<PinHistories>();
            SchoolClassAllocations = new HashSet<SchoolClassAllocations>();
            SchoolDeficiencies = new HashSet<SchoolDeficiencies>();
            SchoolFacilities = new HashSet<SchoolFacilities>();
            SchoolPayments = new HashSet<SchoolPayments>();
            SchoolStaffProfiles = new HashSet<SchoolStaffProfiles>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? OfficeId { get; set; }
        public Guid? LgId { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public long? YearEstablished { get; set; }
        public bool IsRecognised { get; set; }
        public bool IsVetted { get; set; }
        public bool IsInspected { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsRecommended { get; set; }
        public bool HasDeficientSubject { get; set; }
        public bool HasDeficientFacilitiy { get; set; }

        public virtual SchoolCategories Category { get; set; }
        public virtual LocalGovernments Lg { get; set; }
        public virtual Offices Office { get; set; }
        public virtual ICollection<PinHistories> PinHistories { get; set; }
        public virtual ICollection<SchoolClassAllocations> SchoolClassAllocations { get; set; }
        public virtual ICollection<SchoolDeficiencies> SchoolDeficiencies { get; set; }
        public virtual ICollection<SchoolFacilities> SchoolFacilities { get; set; }
        public virtual ICollection<SchoolPayments> SchoolPayments { get; set; }
        public virtual ICollection<SchoolStaffProfiles> SchoolStaffProfiles { get; set; }
    }
}
