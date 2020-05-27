using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class ApplicationRoles
    {
        public ApplicationRoles()
        {
            ApplicationUsers = new HashSet<ApplicationUsers>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string UrlHome { get; set; }

        public virtual ICollection<ApplicationUsers> ApplicationUsers { get; set; }
    }
}
