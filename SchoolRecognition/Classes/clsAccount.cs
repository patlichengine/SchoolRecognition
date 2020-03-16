using SchoolRecognition.Models;
using SchoolRecognition.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace SchoolRecognition.Classes
{
    public class clsAccount : IAccount
    {
        public void CreateUser(RegisterViewModel _obj)
        {
            using(var con = new clsDBConnection().OpenConnection())
            {
                     // var  result = con.Query<SchoolPaymentViewModel>("dbo.procTertiaryDetailsGet", new { ID = id }, commandType: CommandType.StoredProcedure).ToList();
                    
                                   
            }
        }

       
        public SchoolPaymentViewModel CreatePayment(SchoolPaymentViewModel _model)
        {
            SchoolPaymentViewModel result = new SchoolPaymentViewModel();
            using (var con = new clsDBConnection().OpenConnection())
            {
                 result = con.Query<SchoolPaymentViewModel>("dbo.AddSchoolPayment", new { PinID=_model.PinID,SchoolID=_model.SchoolID,Amount=_model.Amount,ReceiptNo=_model.ReceiptNo,ReceiptImage=_model.ReceiptImage }, commandType: CommandType.StoredProcedure).FirstOrDefault();


            }
            return result;
        }
    }
}
