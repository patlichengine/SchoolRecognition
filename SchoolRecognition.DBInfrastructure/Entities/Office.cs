using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class Office
    {
        public Office()
        {
            InverseParent = new HashSet<Office>();
            OfficeStates = new HashSet<OfficeState>();
        }

        public Guid Id { get; set; }
        public string OfficeTitle { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public string ShortName { get; set; }
        public Guid? ParentId { get; set; }
        public string ContactAddress { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsBranch { get; set; }
        public bool? IsZonal { get; set; }

        public virtual Office Parent { get; set; }
        public virtual ICollection<Office> InverseParent { get; set; }
        public virtual ICollection<OfficeState> OfficeStates { get; set; }
    }
}
