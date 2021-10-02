using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class UserGroup
    {
        public UserGroup()
        {
            ApplicationUsers = new HashSet<ApplicationUser>();
        }

        public Guid Id { get; set; }
        public string GroupTitle { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string UrlHome { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
