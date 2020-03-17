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
    public class SchoolCategoriesService : SchoolRecognitionContext, SchoolCategoriesRepo
    {
        
        public Task<int?> Create(SchoolCategories schoolCategories)
        {

            return Task.Run(async () =>
            {
                //declare return var 
                int? queryResult = 0;
                try
                {
                    using (IDbConnection _db = new clsDBConnection().OpenConnection())
                    {
                        //Create a sql parater objects
                        SqlParameter[] commandParameters = new SqlParameter[]
                        {
                            new SqlParameter("@ID", schoolCategories.Id),
                            new SqlParameter("@Name", schoolCategories.Name),
                            new SqlParameter("@ICodeD", schoolCategories.Code),
                        };

                        //Execute the query command
                        queryResult = await _db.ExecuteAsync("procCreateSchoolCategory", commandParameters, commandType: CommandType.StoredProcedure);
                    }
                        

                    //SqlMapper.Execute(con, "Create", param: commandParameters, commandType: CommandType.StoredProcedure);

                    return queryResult;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            );
            
        }

     

        public Task<int> Delete(Guid schoolCategoriesId)
        {
            return Task.Run(async () =>
            {

                try
                {
                    int result = 0;

                    using (IDbConnection myConnection = new clsDBConnection().OpenConnection())
                    {

                        string queryAction = "DELETE FROM dbo.SchoolCategory WHERE ID = @ID";

                        if (schoolCategoriesId != Guid.Empty)
                        {
                            var _result = await myConnection.ExecuteAsync(queryAction, commandType: CommandType.Text);

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
            );            
        }

        public Task<SchoolCategories> GetBySchoolCategoriesId(Guid schoolCategoriesId)
        {

            return Task.Run(async () =>
            {
               
                try
                {
                    using (IDbConnection _db = new clsDBConnection().OpenConnection())
                    {
                       
                                SchoolCategories result = null;

                                if (schoolCategoriesId != Guid.Empty)
                                {

                                    string strQuery = "Select * from procSelectSchoolCategory WHERE ID = @_id;";

                                    var _result = await _db.QueryFirstOrDefaultAsync<SchoolCategories>(strQuery, new { _id = schoolCategoriesId });

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
            );

        }

        
        public Task<int> Update(Models.SchoolCategories schoolCategories)
        {
            return Task.Run(async () =>
            {
                //declare return var 
                int queryResult = 0;
                try
                {
                    using (IDbConnection _db = new clsDBConnection().OpenConnection())
                    {
                        //Create a sql parater objects
                        SqlParameter[] commandParameters = new SqlParameter[]
                        {
                            new SqlParameter("@ID", schoolCategories.Id),
                            new SqlParameter("@Name", schoolCategories.Name),
                            new SqlParameter("@ICodeD", schoolCategories.Code),
                        };

                        //Execute the query command
                        queryResult = await _db.ExecuteAsync("procUpdateSchoolCategory", commandParameters, commandType: CommandType.StoredProcedure);
                    }


                    //SqlMapper.Execute(con, "Create", param: commandParameters, commandType: CommandType.StoredProcedure);

                    return queryResult;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            );
        }

      public Task<List<SchoolCategories>> ListAll()
        {
            return Task.Run(async () =>
            {
                try
                {
                    using (IDbConnection _db = new clsDBConnection().OpenConnection())
                    {
                        var result = new List<SchoolCategories>();

                        string strQuery = "Select * from dbo.SchoolCategories;";

                        var _result = await _db.QueryAsync<SchoolCategories>(strQuery);

                        result = _result.ToList();

                        return result;
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }

            );
        }
    }
}