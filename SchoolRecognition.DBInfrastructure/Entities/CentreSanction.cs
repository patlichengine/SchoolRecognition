using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class CentreSanction
    {
        public Guid Id { get; set; }
        public Guid? SanctionSettingId { get; set; }
        public string Description { get; set; }
        public int NoOfYears { get; set; }
        public int YearOfSaction { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string SubjectCode { get; set; }
        public Guid? SchoolProfileId { get; set; }
        public bool IsDerecognised { get; set; }
        public DateTime DateSanctionStarted { get; set; }

        public virtual ApplicationUser CreatedByNavigation { get; set; }
        public virtual SanctionSetting SanctionSetting { get; set; }
        public virtual SchoolProfile SchoolProfile { get; set; }
    }
}
