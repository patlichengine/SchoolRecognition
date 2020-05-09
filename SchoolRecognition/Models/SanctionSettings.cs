using System;
using System.Collections.Generic;

namespace SchoolRecognition.Models
{
    public partial class SanctionSettings
    {
        public SanctionSettings()
        {
            CentreSanctions = new HashSet<CentreSanctions>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<CentreSanctions> CentreSanctions { get; set; }
    }
}
