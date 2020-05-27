using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class CentresDto
    {
        public Guid Id { get; set; }
        public string CentreNo{ get; set; }
        public string CentreName { get; set; }
        public Guid SchoolCategoryId { get; set; }
        public byte[] CentreImage { get; set; }
        public double Longitude { get; set; }
        public double? Latitude { get; set; }
    }
}
