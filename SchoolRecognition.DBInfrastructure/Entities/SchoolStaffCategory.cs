using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class SchoolStaffCategory
    {
        public SchoolStaffCategory()
        {
            SchoolStaffProfiles = new HashSet<SchoolStaffProfile>();
        }

        public Guid Id { get; set; }
        public string Category { get; set; }
        public byte RankOrder { get; set; }

        public virtual ICollection<SchoolStaffProfile> SchoolStaffProfiles { get; set; }
    }
}
