using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class LocationCategory
    {
        public LocationCategory()
        {
            Locations = new HashSet<Location>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public bool Visibility { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
