using Dapper;
using Microsoft.Data.SqlClient;
using SchoolRecognition.Classes;

using SchoolRecognition.Models;
using SchoolRecognition.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public class SchoolCategoriesService :  SchoolCategoriesRepo
    {
        private readonly ConnectionString _connectionString;

        public SchoolCategoriesService(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> Create(SchoolCategories categories)
        {
            int objResult = 0;
            try
            {
                using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                {
                    objResult = await _db.ExecuteAsync("dbo.AddSchoolCategory", new
                    {
                        Name = categories.Name,
                        Code = categories.Code

                    }, commandType: CommandType.Text);

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return objResult;
        }

        public Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<SchoolCategories> GetById(Guid id)
        {
            SchoolCategories objUsers = new SchoolCategories();
            try
            {

                using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                {
                    objUsers = await _db.QueryFirstAsync<SchoolCategories>("dbo.GetSchoolCategoryByID", new { categoryID = id }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return objUsers;
        }

        public async Task<List<SchoolCategories>> List()
        {
            List<SchoolCategories> resultVal = new List<SchoolCategories>();
            try
            {

                using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                {
                    var result = await _db.QueryAsync<SchoolCategories>("dbo.stpSelectAllSchoolCategories", commandType: CommandType.StoredProcedure);

                    resultVal = result.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return resultVal;
        }

        public Task<int> Update(SchoolCategories categories)
        {
            throw new NotImplementedException();
        }
    }
}