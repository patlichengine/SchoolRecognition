using System;
using System.Collections.Generic;

namespace SchoolRecognition.Models
{
    public partial class Ranks
    {
        public Ranks()
        {
            Users = new HashSet<Users>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
