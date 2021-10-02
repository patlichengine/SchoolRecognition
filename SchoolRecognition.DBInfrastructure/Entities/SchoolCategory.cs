using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class SchoolCategory
    {
        public SchoolCategory()
        {
            SchoolProfiles = new HashSet<SchoolProfile>();
        }

        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }

        public virtual ICollection<SchoolProfile> SchoolProfiles { get; set; }
    }
}
