using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class ApplicationSetting
    {
        public Guid Id { get; set; }
        public byte? MinNoOfRecognitionYears { get; set; }
        public byte? MaxNoOfPinsToGenerate { get; set; }
    }
}
