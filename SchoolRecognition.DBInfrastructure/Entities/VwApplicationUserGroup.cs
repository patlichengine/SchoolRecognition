using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class VwApplicationUserGroup
    {
        public Guid Id { get; set; }
        public string GroupTitle { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string UrlHome { get; set; }
        public string Description { get; set; }
    }
}
