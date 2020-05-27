using SchoolRecognition.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class PinHistoriesDto
    {
        public Guid Id { get; set; }
        public Guid? SchoolId { get; set; }
        public Guid? PinId { get; set; }
        public DateTime? DateActive { get; set; }
        public Guid? CreatedBy { get; set; }

        public  Pins Pin { get; set; }
        public  Schools School { get; set; }
    }

    public class PinHistoriesCreateDto : PinHistoriesManipulateDto
    {
        public DateTime? DateActive { get; set; } = DateTime.Now;
        public Guid? CreatedBy { get; set; }
    }


    public abstract class PinHistoriesManipulateDto
    {
       

        [Required(ErrorMessage = "A School ID is required")]
        public Guid? SchoolId { get; set; }
        [Required(ErrorMessage = "A School Pin is required")]
        public Guid? PinId { get; set; }
        

        public Pins Pin { get; set; }
        public Schools School { get; set; }

    }
}
