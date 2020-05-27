using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class Titles
    {
        public Titles()
        {
            SchoolStaffProfiles = new HashSet<SchoolStaffProfiles>();
        }

        public int Id { get; set; }
        public string Code { get; set; }

        public virtual ICollection<SchoolStaffProfiles> SchoolStaffProfiles { get; set; }
    }
}
