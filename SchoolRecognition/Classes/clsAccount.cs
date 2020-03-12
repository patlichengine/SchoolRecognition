using SchoolRecognition.Models;
using SchoolRecognition.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Classes
{
    public class clsAccount : IAccount
    {
        public void CreateUser(RegisterViewModel _obj)
        {
            using(var con = new clsDBConnection().OpenConnection())
            {

            }
        }
    }
}
