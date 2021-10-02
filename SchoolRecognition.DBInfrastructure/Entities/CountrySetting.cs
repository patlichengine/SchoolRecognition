using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class CountrySetting
    {
        public CountrySetting()
        {
            ApplicationUsers = new HashSet<ApplicationUser>();
        }

        public Guid Id { get; set; }
        public Guid? ApplicationSettingId { get; set; }
        public byte? MinimumTradeSubjects { get; set; }
        public int? CountryCode { get; set; }
        public string CoreSubjects { get; set; }
        public bool IsDefault { get; set; }

        public virtual Country CountryCodeNavigation { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
