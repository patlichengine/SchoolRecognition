using Dapper;
using Microsoft.Data.SqlClient;
using SchoolRecognition.Classes;
using SchoolRecognition.Helpers;
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
        private const string Sql = "stpCreateSchoolCategory";
        private ConnectionString _connectionString;

        public SchoolCategoriesService(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> Create(SchoolCategories schoolCategories)
        {

           
                //declare return var 
                int queryResult = 0;
                try
                {
                    using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                    {

                        //Execute the query command
                        queryResult = await _db.ExecuteAsync(Sql,
                            new
                            {
                                categoryName = schoolCategories.Name,
                                categoryCode = schoolCategories.Code
                            },
                            
                            
                            commandType: CommandType.StoredProcedure);
                    }
                        

                    

                   
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            return queryResult;
        
            
        }

     

        public async Task<int> Delete(Guid schoolCategoriesId)
        {
            

                try
                {
                    int result = 0;

                    using (IDbConnection myConnection = new SqlConnection(_connectionString.Value))
                    {

                        //string queryAction = "DELETE FROM dbo.SchoolCategory WHERE ID = @ID";

                        if (schoolCategoriesId != Guid.Empty)
                        {
                            var _result = await myConnection.ExecuteAsync("stpSchoolCategory", commandType: CommandType.Text);

                            result = _result;
                        }
                        return result;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

          
        }

        public async Task<SchoolCategories> GetBySchoolCategoriesId(Guid schoolCategoriesId)
        {
                SchoolCategories categories = new SchoolCategories();
            
            {
               
               
                try
                {
                    using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                    {
                       
                                

                                if (schoolCategoriesId != Guid.Empty)
                                {

                                    //string strQuery = "Select * from procSelectSchoolCategory WHERE ID = @_id;";

                                    var _result = await _db.QueryFirstOrDefaultAsync<SchoolCategories>("stpSelectSchoolCategoryByID",  new { _id = schoolCategoriesId });

                            categories = _result;
                                }

                                
                    
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return categories;
            }
            

        }

        
        public async Task<int> Update(SchoolCategories schoolCategories)
        {
            
                //declare return var 
                int queryResult = 0;
                try
                {
                    using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                {
                    //Create a sql parater objects

                    queryResult = await _db.ExecuteAsync("stpUpdateSchoolCategory",
                        new
                        {
                            categoryName = schoolCategories.Name,
                            categoryCode = schoolCategories.Code
                        },

                     commandType: CommandType.StoredProcedure);
                    }


                    //SqlMapper.Execute(con, "Create", param: commandParameters, commandType: CommandType.StoredProcedure);

                    
                }
                catch (Exception ex)
                {
                Console.WriteLine(ex.Message);
            }
            return queryResult;
        }

      public async Task<List<SchoolCategories>> ListAll()
        {
            List<SchoolCategories> result = new List<SchoolCategories>();
                try
                {
                    using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                {
                       

                        var _result = await _db.QueryAsync<SchoolCategories>("stpSelectAllSchoolCategories", commandType: CommandType.StoredProcedure
                            
                            
                            );

                        result = _result.ToList();

                       
                    }
                }
                catch(Exception ex)
                {
                Console.WriteLine(ex.Message);
                }
            return result;
        }
    }
}