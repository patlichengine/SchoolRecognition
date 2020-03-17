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
            //using(var con = new clsDBConnection().OpenConnection())
            //{
            //         // var  result = con.Query<SchoolPaymentViewModel>("dbo.procTertiaryDetailsGet", new { ID = id }, commandType: CommandType.StoredProcedure).ToList();
                    
                                   
            //}
        }

       
      
    }
}
