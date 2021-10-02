using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class ApprovedSchoolClass
    {
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public short? Position { get; set; }
    }
}
