﻿using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class Users
    {
        public Users()
        {
            AuditTrail = new HashSet<AuditTrail>();
            FacilitySettings = new HashSet<FacilitySettings>();
            Offices = new HashSet<Offices>();
            PinHistories = new HashSet<PinHistories>();
            Pins = new HashSet<Pins>();
            SchoolFacilities = new HashSet<SchoolFacilities>();
            SchoolPayments = new HashSet<SchoolPayments>();
            SchoolStaffDegrees = new HashSet<SchoolStaffDegrees>();
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
        public virtual ICollection<AuditTrail> AuditTrail { get; set; }
        public virtual ICollection<FacilitySettings> FacilitySettings { get; set; }
        public virtual ICollection<Offices> Offices { get; set; }
        public virtual ICollection<PinHistories> PinHistories { get; set; }
        public virtual ICollection<Pins> Pins { get; set; }
        public virtual ICollection<SchoolFacilities> SchoolFacilities { get; set; }
        public virtual ICollection<SchoolPayments> SchoolPayments { get; set; }
        public virtual ICollection<SchoolStaffDegrees> SchoolStaffDegrees { get; set; }
    }
}