using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class Centres
    {
        public Guid Id { get; set; }
        public string CentreNo { get; set; }
        public string CentreName { get; set; }
        public Guid? SchoolCategoryId { get; set; }
        public byte[] CentreImage { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

        public virtual SchoolCategories SchoolCategory { get; set; }
    }
}
