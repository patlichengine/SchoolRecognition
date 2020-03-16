using Dapper;
using Microsoft.Data.SqlClient;
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
    public class SchoolCategoriesService : BaseRepo, SchoolCategoriesRepo
    {
        
        public bool Create(Models.SchoolCategories schoolCategories)
        {
            try
            {
                DynamicParameters dbPara = new DynamicParameters();
                dbPara.Add("@ID", SqlDbType.UniqueIdentifier);
                //dbPara.Add("@ID", schoolCategories.Id);
                dbPara.Add("@Name", schoolCategories.Name);
                dbPara.Add("@Code", schoolCategories.Code);
                //dbPara.Add("CreatedBY", "1", DbType.String);
                //dbPara.Add("CreatedDateTime", DateTime.Now, DbType.DateTime);
                //dbPara.Add("UpdatedBY", "1", DbType.String);
                //dbPara.Add("UpdatedDateTime", DateTime.Now, DbType.DateTime);

               SqlMapper.Execute(con, "Create", param: dbPara, commandType: CommandType.StoredProcedure);
                
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public bool Delete(Guid schoolCategoriesId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", schoolCategoriesId);
            SqlMapper.Execute(con, "DeleteUser", param: parameters, commandType: CommandType.StoredProcedure);
            return true;
            
        }

        public Models.SchoolCategories GetBySchoolCategoriesId(Guid schoolCategoriesId)
        {

            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerID", schoolCategoriesId);

          var result =      SqlMapper.Query<SchoolCategories>((SqlConnection)con, "GetUserById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public IList<SchoolCategories> ListAll() => SqlMapper.Query<SchoolCategories>(con, "Create", commandType: CommandType.StoredProcedure).ToList();

        
        public bool Update(Models.SchoolCategories schoolCategories)
        {
            try
            {


                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID", SqlDbType.UniqueIdentifier);
                parameters.Add("@Name", schoolCategories.Name);
                parameters.Add("@Code", schoolCategories.Code);
                //dbPara.Add("CreatedBY", "1", DbType.String);
                //dbPara.Add("CreatedDateTime", DateTime.Now, DbType.DateTime);
                //dbPara.Add("UpdatedBY", "1", DbType.String);
                //dbPara.Add("UpdatedDateTime", DateTime.Now, DbType.DateTime);

                SqlMapper.Execute(con, "Update", param: parameters, commandType: CommandType.StoredProcedure);
                return true;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}