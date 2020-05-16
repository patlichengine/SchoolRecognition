using SchoolRecognition.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class PinViewPagedListPinHistoriesDto
    {
        public Guid Id { get; set; }
        public String RecognitionTypeName { get; set; }
        public string SerialNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsInUse { get; set; }
        public String CreatedByUser { get; set; }
        public DateTime? DateCreated { get; set; }
        public string AssignedSchoolName
        {
            get
            {
                string schoolAssigned = null;

                if (Payments != null && Payments.Count() > 0)
                {
                    var schoolPayment = Payments.SingleOrDefault();
                    if (schoolPayment != null)
                    {
                        schoolAssigned = $"{schoolPayment.SchoolName}";
                    }
                }

                return schoolAssigned;
            }
        }
        public string AssignedSchoolCategoryName
        {
            get
            {
                string schoolAssigned = null;

                if (Payments != null && Payments.Count() > 0)
                {
                    var schoolPayment = Payments.SingleOrDefault();
                    if (schoolPayment != null)
                    {
                        schoolAssigned = $"{schoolPayment.SchoolCategoryName}";
                    }
                }

                return schoolAssigned;
            }
        }

        public IEnumerable<SchoolPaymentsViewDto> Payments { get; set; }
        public CustomPagedList<PinHistoriesViewDto> Histories { get; set; }
        //public virtual ICollection<SchoolPayments> SchoolPayments { get; set; }
    }
}
