using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Helpers
{
    public class BaseRepo : IDisposable
    {
        protected IDbConnection con;
        public BaseRepo()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=SchoolRecognition;Integrated Security=True";
            con = new SqlConnection(connectionString);
        }

      
        public void Dispose()
        {
          //  throw new NotImplementedException();
        }
    }
}
