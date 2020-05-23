using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class SchoolStaffProfiles
    {
        public SchoolStaffProfiles()
        {
            SchoolStaffDegrees = new HashSet<SchoolStaffDegrees>();
            SchoolStaffSubjects = new HashSet<SchoolStaffSubjects>();
        }

        public Guid Id { get; set; }
        public Guid? SchoolId { get; set; }
        public int? TitleId { get; set; }
        public string Surname { get; set; }
        public string Othernames { get; set; }
        public string ContactAddress { get; set; }
        public DateTime? DateEmployed { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public Guid? CategoryId { get; set; }
        public bool? HasTrcn { get; set; }
        public string Trcn { get; set; }
        public bool IsBarred { get; set; }

        public virtual SchoolStaffCategories Category { get; set; }
        public virtual Schools School { get; set; }
        public virtual Titles Title { get; set; }
        public virtual ICollection<SchoolStaffDegrees> SchoolStaffDegrees { get; set; }
        public virtual ICollection<SchoolStaffSubjects> SchoolStaffSubjects { get; set; }
    }
}
