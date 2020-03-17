using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Repository
{
    interface IPayment
    {
        public SchoolPaymentViewModel AddSchoolPayment(SchoolPaymentViewModel model);
        //public void UpdateSchoolPayment(Guid id);
        //public void DeleteSchoolPayment(Guid id);
        //public SchoolPaymentViewModel GetSchoolPayments();
        //public SchoolPaymentViewModel GetSchoolPayment(Guid id);
    }
}
