using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class EmailSettings
    {
        public string MailServer { get; set; } = "smtp.gmail.com";
        public int MailPort { get; set; } = 25;
        public string SenderName { get; set; } = "The West African Examinations Council";
        public string Sender { get; set; } = "ictd.waec02@gmail.com";
        public string Password { get; set; } = "waec12345";
    }
}
