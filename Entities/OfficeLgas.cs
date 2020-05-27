using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Entities
{
    public partial class OfficeLgas
    {
       
        public Guid Id { get; set; }
        public Guid? OfficeID { get; set; }
        public Guid? LgaID { get; set; }
        public virtual Offices Office { get; set; }
        public virtual LocalGovernments Lga { get; set; }

    }
}
