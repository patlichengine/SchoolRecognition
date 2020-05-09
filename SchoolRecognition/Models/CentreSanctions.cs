using System;
using System.Collections.Generic;

namespace SchoolRecognition.Models
{
    public partial class CentreSanctions
    {
        public Guid Id { get; set; }
        public Guid? SanctionSettingId { get; set; }
        public Guid? CentreId { get; set; }
        public string Description { get; set; }
        public int NoOfYears { get; set; }
        public int YearOfSanction { get; set; }

        public virtual Centres Centre { get; set; }
        public virtual SanctionSettings SanctionSetting { get; set; }
    }
}
