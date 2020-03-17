using SchoolRecognition.Models;
using SchoolRecognition.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
namespace SchoolRecognition.Classes
{
    public class clsContent:IPayment
    {
        //private static IDbConnection OpenConnection()
        //{
        //    string constr = ConfigurationManager.ConnectionString["DapperConnection"].ToString();
        //}
        public SchoolPaymentViewModel AddSchoolPayment(SchoolPaymentViewModel _model)
        {
            SchoolPaymentViewModel result = new SchoolPaymentViewModel();
            using (var con = new clsDBConnection().OpenConnection())
            {

                result = con.Query<SchoolPaymentViewModel>("dbo.AddSchoolPayment", new { PinID = _model.PinID, SchoolID = _model.SchoolID, Amount = _model.Amount, ReceiptNo = _model.ReceiptNo, ReceiptImage = _model.ReceiptImage }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
            return result;
        }
        public static List<RecognitionTypes> GetAllRecognitionType()
        {
            List<RecognitionTypes> result = null;
            using (var con = new clsDBConnection().OpenConnection())
            {

                result = con.Query<RecognitionTypes>("dbo.GetAllRecognitionType", commandType: CommandType.StoredProcedure).ToList();

            }
            return result;
        }
    }
}
