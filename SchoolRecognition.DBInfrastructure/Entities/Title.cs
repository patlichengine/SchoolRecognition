using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class Title
    {
        public Title()
        {
            SchoolStaffProfiles = new HashSet<SchoolStaffProfile>();
        }

        public int Id { get; set; }
        public string TitleName { get; set; }

        public virtual ICollection<SchoolStaffProfile> SchoolStaffProfiles { get; set; }
    }
}
