using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class Country
    {
        public Country()
        {
            CountrySettings = new HashSet<CountrySetting>();
            SchoolProfiles = new HashSet<SchoolProfile>();
        }

        public int CountryCode { get; set; }
        public string RecType { get; set; }
        public string CountryName { get; set; }
        public byte MinSubjects { get; set; }
        public byte MaxSubjects { get; set; }
        public byte Casscycle { get; set; }
        public string FederalUnit { get; set; }
        public string LocalUnit { get; set; }
        public string PhoneCode { get; set; }
        public byte[] CoatOfArm { get; set; }
        public byte[] Flag { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsAvailable { get; set; }
        public string ModifiedIp { get; set; }
        public int LowerAgeLimit { get; set; }
        public int UpperAgeLimit { get; set; }

        public virtual ICollection<CountrySetting> CountrySettings { get; set; }
        public virtual ICollection<SchoolProfile> SchoolProfiles { get; set; }
    }
}
