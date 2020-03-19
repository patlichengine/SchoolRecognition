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

    public class clsRecognitionTypes : IRecognitionTypes
    {

        private readonly ConnectionString _connectionString;
        private IPins _pinService;
        public clsRecognitionTypes(ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _pinService = new clsPins(_connectionString);

        }

        public Task<List<RecognitionTypes>> Get()
        {
            return Task.Run(async () =>
            {

                try
                {
                    using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                    {
                        var result = new List<RecognitionTypes>();

                        //string strQuery = "Select * from dbo.RecognitionTypes;";
                        string strQuery = "SELECT * FROM dbo.RecognitionTypes AS recognitionType;";

                        var _result = await _db.QueryAsync<RecognitionTypes>(strQuery);

                        result = _result.ToList();

                        return result;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            });
        }
        public Task<RecognitionTypes> Get(Guid id)
        {
            return Task.Run(async () =>
            {

                try
                {
                    using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                    {
                        RecognitionTypes result = null;
                        List<Pins> _pins = new List<Pins>();

                        if (id != Guid.Empty)
                        {
                            //string strQuery = "Select * from dbo.RecognitionTypes WHERE ID = @_id;";
                            string strQuery = "SELECT * FROM dbo.RecognitionTypes WHERE Id = @_id;";

                            var _result = await _db.QueryFirstOrDefaultAsync<RecognitionTypes>(strQuery, new { _id = id });

                            if (_result != null)
                            {
                                var pins = await _pinService.GetPinsByRecognitionTypeId(id);

                                result = _result;
                                result.Pins = pins.ToList();

                            }

                            return result;

                        }

                        return result;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            });
        }
        public Task<Guid?> Create(RecognitionTypes _obj)
        {

            return Task.Run(async () =>
            {

                try
                {
                    using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                    {
                        string strQuery = "INSERT INTO dbo.RecognitionTypes (Id,Name,Code)" +
                        " VALUES (@Id,@Name,@Code);";

                        Guid? returnId = Guid.Empty;
                        if (_obj != null)
                        {
                            _obj.Id = Guid.NewGuid();

                            var _result = await _db.ExecuteAsync(strQuery, new
                            {
                                Id = _obj.Id,
                                Name = _obj.Name,
                                Code = _obj.Code
                            }, commandType: CommandType.Text);

                            returnId = _obj.Id;
                        }

                        return returnId;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            });
        }
        public Task<RecognitionTypes> Update(RecognitionTypes _obj)
        {

            return Task.Run(async () =>
            {

                try
                {
                    using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                    {
                        string strQuery = "UPDATE INTO dbo.RecognitionTypes SET " +
                        "Name = @Name, " +
                        "Code = @Code, " +
                        "WHERE Id = @Id;";

                        Guid? returnId = Guid.Empty;
                        if (_obj != null)
                        {

                            var _result = await _db.ExecuteAsync(strQuery, new
                            {
                                Id = _obj.Id,
                                Name = _obj.Name,
                                Code = _obj.Code
                                //CreatedBy = _obj.CreatedBy,
                                //DateCreated = _obj.DateCreated
                            });

                        }

                        return _obj;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            });
        }
        public Task Delete(Guid id) //return type is void
        {

            return Task.Run(async () =>
            {

                try
                {
                    using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                    {
                        string strQuery = "DELETE FROM dbo.RecognitionTypes WHERE Id = @Id";

                        if (id != Guid.Empty)
                        {
                            var _result = await _db.ExecuteAsync(strQuery, commandType: CommandType.Text);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            });
        }


    }
}
