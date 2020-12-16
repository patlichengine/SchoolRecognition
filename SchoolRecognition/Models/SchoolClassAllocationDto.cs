using SchoolRecognition.Entities;
using System;

namespace SchoolRecognition.Models
{
    public class SchoolClassAllocationDto
    {
        public Guid Id { get; set; }
        public Guid ClassId { get; set; }
        public Guid SchoolId { get; set; }
        public byte NoOfArms { get; set; }
        public int TotalStudents { get; set; }

        public string SchoolClass { get; set; }
        public string SchoolName { get; set; }
     
    }
}