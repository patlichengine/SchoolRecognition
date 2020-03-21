using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using SchoolRecognition.Models;
using SchoolRecognition.Repository;

namespace SchoolRecognition.Classes
{
    public class clsOffices : IOffices
    {
        private ConnectionString _connectionString;

        public clsOffices()
        {
        }

        public clsOffices(ConnectionString connectionString)
        {
            _connectionString = connectionString;

        }

        //Create offices method implementation
        public async Task<int> Create(Offices offices) 
        {
            int queryResult = 0;
            try 
            {
                using (IDbConnection _db = new SqlConnection(_connectionString.Value)) 
                {
                    queryResult = await _db.ExecuteAsync("AddOffices",
                        new
                        {
                            officeName = offices.Name,
                            officeAddress = offices.Address,
                            
                            officeDateCreated = offices.DateCreated,
                            createdBy = offices.CreatedBy
                          

                        },
                      commandType: CommandType.StoredProcedure);
                
                }
            
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return queryResult;
        }



        //Delete offices method implementation

        public async Task<int> Delete(Guid officesId)
        {
            throw new NotImplementedException();
        }


        //Get offices by ID method implementation
        public async Task<Offices> GetByOfficesid(Guid officesId)
        {
            Offices offices = new Offices();
            try
            {
                using(IDbConnection db = new SqlConnection(_connectionString.Value))
                {
                    offices = await db.QueryFirstAsync<Offices>("stpOfficesById", new
                    {
                        ID = officesId
                    }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex){
                Console.WriteLine(ex.Message);
            }
            return offices;
        }


        //List offices method implementation
        public async Task<List<Offices>> ListAll()
        {
            
            try
            {
                List<Offices> result = new List<Offices>();
                using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                {
                    var _result = await _db.QueryAsync<Offices>("dbo.stpSelectAllOffices", commandType: CommandType.StoredProcedure);

                    result = _result.ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


        //update offices method implementation
        public async Task<int> Update(Offices offices)
        {
            int queryResult = 0;
            try
            {
                using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                {
                    queryResult = await _db.ExecuteAsync("",
                        new
                        {
                            officeAddress = offices.Address,
                            officeName = offices.Name,
                            officeDateCreated = offices.DateCreated

                        },
                      commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return queryResult;
        }
    }

}
