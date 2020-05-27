using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class OfficeLgasDto
    {
        public Guid Id { get; set; }
        public Guid? OfficeID { get; set; }
        public Guid? LgaID { get; set; }

        public string OfficeName { get; set; }

        public string LgaName { get; set; }



    }
}
