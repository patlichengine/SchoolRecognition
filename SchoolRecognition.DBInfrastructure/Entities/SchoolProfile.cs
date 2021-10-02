using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class SchoolProfile
    {
        public SchoolProfile()
        {
            CentreSanctions = new HashSet<CentreSanction>();
            PinHistories = new HashSet<PinHistory>();
            SchoolPayments = new HashSet<SchoolPayment>();
            SchoolStaffProfiles = new HashSet<SchoolStaffProfile>();
        }

        public Guid Id { get; set; }
        public string SchoolName { get; set; }
        public int? LgaId { get; set; }
        public int? CountryCode { get; set; }
        public string CategoryCode { get; set; }
        public string SchoolAddress { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public long? YearEstablished { get; set; }
        public bool IsRecognised { get; set; }
        public string RecognizedSubjects { get; set; }
        public string CentreNo { get; set; }
        public Guid? OfficeId { get; set; }

        public virtual SchoolCategory CategoryCodeNavigation { get; set; }
        public virtual Country CountryCodeNavigation { get; set; }
        public virtual Lga Lga { get; set; }
        public virtual ICollection<CentreSanction> CentreSanctions { get; set; }
        public virtual ICollection<PinHistory> PinHistories { get; set; }
        public virtual ICollection<SchoolPayment> SchoolPayments { get; set; }
        public virtual ICollection<SchoolStaffProfile> SchoolStaffProfiles { get; set; }
    }
}
