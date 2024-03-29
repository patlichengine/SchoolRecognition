﻿using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class SchoolDeficiencies
    {
        public Guid Id { get; set; }
        public Guid? SchoolId { get; set; }
        public string Description { get; set; }
        public Guid? SchoolFacilityId { get; set; }

        public virtual Schools School { get; set; }
        public virtual SchoolFacilities SchoolFacility { get; set; }
    }
}
