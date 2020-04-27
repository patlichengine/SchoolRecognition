﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class PinsCreateDto
    {
        public Guid RecognitionTypeId { get; set; }
        public int NoOfPinToGenerate { get; set; }
        public bool IsActive { get; set; }
        public Guid? CreatedBy { get; internal set; }
    }
}