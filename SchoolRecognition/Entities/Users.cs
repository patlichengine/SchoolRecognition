using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class Users
    {
        public Users()
        {
            Pins = new HashSet<Pins>();
            SchoolPayments = new HashSet<SchoolPayments>();
        }

        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string Othernames { get; set; }
        public byte[] Password { get; set; }
        public Guid? RankId { get; set; }
        public Guid? RoleId { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public string Lpno { get; set; }
        public byte[] Signature { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }

        public virtual Ranks Rank { get; set; }
        public virtual Roles Role { get; set; }
        public virtual ICollection<Pins> Pins { get; set; }
        public virtual ICollection<SchoolPayments> SchoolPayments { get; set; }
    }
}
