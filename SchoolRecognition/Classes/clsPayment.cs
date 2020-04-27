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
using Microsoft.Data.SqlClient;

namespace SchoolRecognition.Classes
{
    public class clsPayment:IPayment
    {
        //public static IDbConnection OpenConnection()
        //{
        //    string constr = ConfigurationManager.ConnectionString["DapperConnection"].ToString();
        //}
        private readonly ConnectionString _connectionString;

        public clsPayment(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }
        public SchoolPaymentViewModel AddSchoolPayment(SchoolPaymentViewModel _model)
        {
            SchoolPaymentViewModel result = new SchoolPaymentViewModel();
            using (var con = new SqlConnection(_connectionString.Value))
            {

                result = con.Query<SchoolPaymentViewModel>("dbo.AddSchoolPayment", new { PinID = _model.PinID, SchoolID = _model.SchoolID, Amount = _model.Amount, ReceiptNo = _model.ReceiptNo, ReceiptImage = _model.ReceiptImage }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
            return result;
        }
        public async Task<List<RecognitionTypes>> GetAllRecognitionType()
        {
            List<RecognitionTypes> resultVal = new List<RecognitionTypes>();
            try
            {
                
                using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                {
                    var result = await _db.QueryAsync<RecognitionTypes>("dbo.GetAllRecognitionType", commandType: CommandType.StoredProcedure);

                    resultVal = result.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return resultVal;
            //List<RecognitionTypes> result = null;
            //using (IDbConnection con = new clsDBConnection().OpenConnection())
            //{

            //    result = con.Query<RecognitionTypes>("dbo.GetAllRecognitionType", commandType: CommandType.StoredProcedure).ToList();

            //}
            //return result;
        }
    }
}
