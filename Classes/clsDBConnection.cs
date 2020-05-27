using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Classes
{
    public class clsDBConnection
    {
        private readonly ConnectionString _connectionString;

        //public clsDBConnection()
        //{

        //}
        public clsDBConnection(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }
        
        //To Handle connection related activities
        public IDbConnection OpenConnection()
        {
            string constr = _connectionString.Value;       //.GetConnectionString("myDapperConnection").ToString();
            return new SqlConnection(constr);
        }

        public SqlConnection DbConnection()
        {
            string constr = _connectionString.Value;        // _configuration.GetConnectionString("myDapperConnection").ToString();
            return new SqlConnection(constr);
        }
    }
}
