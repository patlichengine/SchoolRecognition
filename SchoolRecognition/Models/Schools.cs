using System;
using System.Collections.Generic;

namespace SchoolRecognition.Models
{
    public partial class Schools
    {
        public Schools()
        {
            PinHistories = new HashSet<PinHistories>();
            SchoolPayments = new HashSet<SchoolPayments>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? OfficeId { get; set; }
        public Guid? LgId { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public long? YearEstablished { get; set; }

        public virtual SchoolCategories Category { get; set; }
        public virtual LocalGovernments Lg { get; set; }
        public virtual Offices Office { get; set; }
        public virtual ICollection<PinHistories> PinHistories { get; set; }
        public virtual ICollection<SchoolPayments> SchoolPayments { get; set; }
    }
}
