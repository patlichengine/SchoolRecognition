using System;
using System.Collections.Generic;

namespace SchoolRecognition.Models
{
    public partial class SecurityConfig
    {
        public long Id { get; set; }
        public string SupportedBrowsers { get; set; }
        public int LocalDataRetentionPeriod { get; set; }
        public string OrganisationName { get; set; }
        public byte[] Logo { get; set; }
        public string Address { get; set; }
        public string ClientAllowedIp { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string SupportedClientVersion { get; set; }
    }
}
