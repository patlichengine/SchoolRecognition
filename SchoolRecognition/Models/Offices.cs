using System;
using System.Collections.Generic;

namespace SchoolRecognition.Models
{
    public partial class Offices
    {
        public Offices()
        {
            Schools = new HashSet<Schools>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Guid? StateId { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CreatedBy { get; set; }

        public virtual ICollection<Schools> Schools { get; set; }
    }
}
