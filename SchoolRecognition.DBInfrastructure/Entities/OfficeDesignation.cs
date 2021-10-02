using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class OfficeDesignation
    {
        public Guid Id { get; set; }
        public string DesignationTitle { get; set; }
    }
}
