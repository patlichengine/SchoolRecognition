using System;
using System.Collections.Generic;

namespace SchoolRecognition.Models
{
    public partial class SchoolCategories
    {
        public SchoolCategories()
        {
            Schools = new HashSet<Schools>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Schools> Schools { get; set; }
    }
}
