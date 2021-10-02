using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class VwApplicationUser
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string Othernames { get; set; }
        public byte[] Password { get; set; }
        public int? RankId { get; set; }
        public Guid? UserGroupId { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public string Lpno { get; set; }
        public byte[] Signature { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
        public string GroupTitle { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActiveGroup { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string UrlHome { get; set; }
        public string Description { get; set; }
        public bool IsLocked { get; set; }
        public bool IsFirstLogin { get; set; }
        public string RankShortName { get; set; }
        public string RankTitle { get; set; }
    }
}
