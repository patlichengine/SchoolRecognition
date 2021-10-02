using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class UserSecureAccount
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? SecurityQuestionId { get; set; }
        public byte[] SecurityAnswer { get; set; }
    }
}
