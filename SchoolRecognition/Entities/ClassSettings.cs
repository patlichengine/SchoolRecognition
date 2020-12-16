using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolRecognition.Entities
{
    public partial class ClassSettings
    {
        public ClassSettings()
        {
            SchoolClasses = new HashSet<SchoolClasses>();
            SchoolStaffSubjects = new HashSet<SchoolStaffSubjects>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A Settings name is required")]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<SchoolClasses> SchoolClasses { get; set; }
        public virtual ICollection<SchoolStaffSubjects> SchoolStaffSubjects { get; set; }
    }
}
