using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class SchoolStaffCategories
    {
        public SchoolStaffCategories()
        {
            SchoolStaffProfiles = new HashSet<SchoolStaffProfiles>();
        }

        public Guid Id { get; set; }
        public string Category { get; set; }
        public byte RankOrder { get; set; }

        public virtual ICollection<SchoolStaffProfiles> SchoolStaffProfiles { get; set; }
    }
}
