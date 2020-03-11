using System;
using System.Collections.Generic;

namespace SchoolRecognition.Models
{
    public partial class Roles
    {
        public Roles()
        {
            Users = new HashSet<Users>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
