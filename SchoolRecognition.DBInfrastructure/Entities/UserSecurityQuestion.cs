using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class UserSecurityQuestion
    {
        public Guid Id { get; set; }
        public string SecurityQuestion { get; set; }
    }
}
