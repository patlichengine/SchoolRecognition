using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class DegreeType
    {
        public DegreeType()
        {
            Degrees = new HashSet<Degree>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Degree> Degrees { get; set; }
    }
}
