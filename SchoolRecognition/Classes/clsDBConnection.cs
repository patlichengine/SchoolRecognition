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
        private readonly IConfiguration _configuration;

        public clsDBConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        //To Handle connection related activities
        public IDbConnection OpenConnection()
        {
            string constr = _configuration.GetConnectionString("myDapperConnection").ToString();
            return new SqlConnection(constr);
        }

        public SqlConnection DbConnection()
        {
            string constr = _configuration.GetConnectionString("myDapperConnection").ToString();
            return new SqlConnection(constr);
        }
    }
}
