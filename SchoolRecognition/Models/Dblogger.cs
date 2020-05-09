using System;
using System.Collections.Generic;

namespace SchoolRecognition.Models
{
    public partial class Dblogger
    {
        public long Id { get; set; }
        public string ErrorMsg { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
