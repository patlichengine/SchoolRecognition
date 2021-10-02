using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class SanctionSetting
    {
        public SanctionSetting()
        {
            CentreSanctions = new HashSet<CentreSanction>();
        }

        public Guid Id { get; set; }
        public string SanctionName { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<CentreSanction> CentreSanctions { get; set; }
    }
}
