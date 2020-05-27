using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class RolesDto
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string UrlHome { get; set; }
    }
}
