using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class Lga
    {
        public Lga()
        {
            OfficeLocalGovernments = new HashSet<OfficeLocalGovernment>();
            SchoolProfiles = new HashSet<SchoolProfile>();
        }

        public int Id { get; set; }
        public string LgaName { get; set; }
        public string LocalCode { get; set; }
        public string StateCode { get; set; }

        public virtual ICollection<OfficeLocalGovernment> OfficeLocalGovernments { get; set; }
        public virtual ICollection<SchoolProfile> SchoolProfiles { get; set; }
    }
}
