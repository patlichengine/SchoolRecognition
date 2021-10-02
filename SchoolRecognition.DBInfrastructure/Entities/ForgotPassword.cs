using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class ForgotPassword
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string OtherNames { get; set; }
        public string Email { get; set; }
        public string Lpno { get; set; }
        public byte[] Password { get; set; }
    }
}
