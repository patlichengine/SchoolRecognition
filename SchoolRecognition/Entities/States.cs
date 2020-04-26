using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class States
    {
        public States()
        {
            LocalGovernments = new HashSet<LocalGovernments>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<LocalGovernments> LocalGovernments { get; set; }
    }
}
