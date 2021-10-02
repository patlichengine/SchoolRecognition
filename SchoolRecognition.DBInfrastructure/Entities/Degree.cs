using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class Degree
    {
        public int Id { get; set; }
        public string DegreeName { get; set; }
        public bool IsActive { get; set; }
        public int? DegreeTypeId { get; set; }

        public virtual DegreeType DegreeType { get; set; }
    }
}
