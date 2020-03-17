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
    public class clsAccount : IAccount
    {
        private readonly ConnectionString _connectionString;
        public clsAccount(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> AssignRole(Guid user_id, Guid role_id)
        {
            int objResult = 0;
            try
            {
                using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                {
                    objResult = await _db.ExecuteAsync("procAssignUserRole", new
                    {
                        UserID = user_id,
                        UserGroupID = role_id
                    }, commandType: CommandType.Text);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return objResult;
        }

        public async Task<int> CreateUser(RegisterViewModel _obj)
        {
            
            int objResult = 0;
            try
            {
                using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                {
                    objResult = await _db.ExecuteAsync("procCreateUser", new
                    {
                        Surname = _obj.Surname,
                        OtherName = _obj.OtherName,
                        Email = _obj.Email,
                        Password = _obj.Password,
                        PhoneNo = _obj.PhoneNo
                    }, commandType: CommandType.Text);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return objResult;
        }

        public async Task<Users> Get(Guid id)
        {
            Users objUsers = new Users();
            try
            {

                using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                {
                    objUsers = await _db.QueryFirstAsync<Users>("dbo.procGetUser", new { ID = id }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return objUsers;
        }

        public async Task<List<Users>> List()
        {
            List<Users> resultVal = new List<Users>();
            try
            {

                using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                {
                    var result = await _db.QueryAsync<Users>("dbo.procGetAllUsers", commandType: CommandType.StoredProcedure);

                    resultVal = result.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return resultVal;
        }

        public async Task<List<Users>> List(Guid userGroupID)
        {
            List<Users> resultVal = new List<Users>();
            try
            {

                using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                {
                    var result = await _db.QueryAsync<Users>("dbo.procGetGroupUsers", new { UserGroupID = userGroupID }, commandType: CommandType.StoredProcedure);

                    resultVal = result.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return resultVal;
        }
    }
}
