using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class SchoolCategories
    {
        public SchoolCategories()
        {
            Centres = new HashSet<Centres>();
            Schools = new HashSet<Schools>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Centres> Centres { get; set; }
        public virtual ICollection<Schools> Schools { get; set; }
    }
}
