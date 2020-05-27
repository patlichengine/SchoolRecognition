using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class ApplicationSettings
    {
        public Guid Id { get; set; }
        public byte? MinimumNoOfRecogYears { get; set; }
        public byte? MaximumNoOfPinsToGenerate { get; set; }
        public byte? MinimumSchoolSubjects { get; set; }
        public byte? MinimumTradeSubjects { get; set; }
        public byte? MaximumCoreSubjects { get; set; }
    }
}
