using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class ApplicationSettingsCreateDto
    {
        public Guid Id { get; set; }
        public byte? MinimumNoOfRecogYears { get; set; }
        public byte? MaximumNoOfPinsToGenerate { get; set; }
        public byte? MinimumSchoolSubjects { get; set; }
        public byte? MinimumTradeSubjects { get; set; }
        public byte? MaximumCoreSubjects { get; set; }
    }
}
