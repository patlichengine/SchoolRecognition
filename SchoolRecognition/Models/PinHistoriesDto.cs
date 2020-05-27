using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{

    public class PinHistoriesViewDto
    {
        public Guid Id { get; set; }
        public DateTime? DateActive { get; set; }
        //CreatedByNavigation
        public String CreatedByUser { get; set; }
        //School
        public String SchoolName { get; set; }
        public String SchoolCategoryName { get; set; }
        //Pin
        public String PinSerialNumber { get; set; }
        public String PinRecognitionTypeName { get; set; }
        public String PinRecognitionTypeCode { get; set; }

    }
}
