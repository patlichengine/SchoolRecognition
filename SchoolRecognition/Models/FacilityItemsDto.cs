using SchoolRecognition.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class FacilityItemsDto
    {
      
        public Guid Id { get; set; }

        [DisplayName("Facility Description")]
        public string Description { get; set; }

        [DisplayName("Set Facility Active Status")]
        [UIHint("IsActive")]
        public bool IsActive { get; set; }

        [DisplayName("Set Facility Summary Status")]
        [UIHint("IsSummary")]
        public bool IsSummary { get; set; }

        public virtual IEnumerable<FacilitySettings> facilitySettings { get; set; }

    }
}
