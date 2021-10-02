using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class Centre
    {
        public Centre()
        {
            Locations = new HashSet<Location>();
        }

        public string CentreNo { get; set; }
        public string CentreName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
