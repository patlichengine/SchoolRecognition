using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class PinsStatisticsSummaryDto
    {
        public Int64 PinsCount { get; set; }
        public Int64 IsActivePinsCount { get; set; }
        public Int64 IsInUsePinsCount { get; set; }
    }
}
