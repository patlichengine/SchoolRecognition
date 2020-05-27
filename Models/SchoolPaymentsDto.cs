using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class RanksDto
    {
        public Guid Id { get; set; }
        public Guid PinId { get; set; }
        public Guid SchoolId { get; set; }
        public decimal Amount { get; set; }
        public string ReceiptNo { get; set; }
        public byte[] ReceiptImage { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
