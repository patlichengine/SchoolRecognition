using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class CentresViewDto
    {
        public Guid Id { get; set; }
        public string CentreNo{ get; set; }
        public string CentreName { get; set; }
        public Guid SchoolCategoryId { get; set; }
        public byte[] CentreImage { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string CentreCategoryCode { get; set; }
        //
        public string CreatedByUser { get; set; }
        public string SchoolCategoryName { get; set; }
        public string SchoolCategoryCode { get; set; }
        public bool? IsActive { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public int TotalActiveCentreSanctions { get; set; }
    }
    public class CentresCreateDto
    {
        public Guid Id { get; set; }
        public string CentreNo { get; set; }
        public string CentreName { get; set; }
        public Guid SchoolCategoryId { get; set; }
        public byte[] CentreImage { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public bool? IsActive { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
