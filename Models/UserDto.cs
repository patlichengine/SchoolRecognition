using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string Othernames { get; set; }
        public byte[] Password { get; set; }
        public Guid RankId { get; set; }
        public Guid RoleId { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public string Lpno { get; set; }
        public byte[] Signature { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
    }
}
