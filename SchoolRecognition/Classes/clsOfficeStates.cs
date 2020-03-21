using Dapper;
using Microsoft.Data.SqlClient;
using SchoolRecognition.Models;
using SchoolRecognition.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Classes
{
    public class clsOfficeStates : IOfficeStates
    {
        private ConnectionString _connectionString;

        public clsOfficeStates(ConnectionString connectionString)
        {
            _connectionString = connectionString;                
        }

        public async Task<List<States>> ListAll()
        {

            try
            {
                List<States> result = new List<States>();
                using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                {
                    var _result = await _db.QueryAsync<States>("dbo.stpSelectAllStates", commandType: CommandType.StoredProcedure);

                    result = _result.ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        public Task<int> Delete(Guid StatesId)
        {
            throw new NotImplementedException();
        }

        public Task<States> GetByStatesid(Guid statesId)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(States states)
        {
            throw new NotImplementedException();
        }

        public Task<int> Create(States states)
        {
            throw new NotImplementedException();
        }
    }
}
