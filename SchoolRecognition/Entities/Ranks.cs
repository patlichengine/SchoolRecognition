using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class Ranks
    {
        public Ranks()
        {
            ApplicationUsers = new HashSet<ApplicationUsers>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<ApplicationUsers> ApplicationUsers { get; set; }
    }
}
