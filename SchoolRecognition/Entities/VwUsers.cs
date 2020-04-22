using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class VwUsers
    {
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
        public string RoleName { get; set; }
        public bool IsDefault { get; set; }
        public bool IsRoleActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
    }
}
