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
                        //string strQuery = "SELECT * FROM dbo.RecognitionTypes AS recognitionType;";

                        var _result = await _db.QueryAsync<RecognitionTypes>("dbo.procRecognitionTypesList", commandType: CommandType.StoredProcedure);

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

                            //var _result = await _db.QueryFirstOrDefaultAsync<RecognitionTypes>(strQuery, new { _id = id });
                            var _result = await _db.QueryFirstOrDefaultAsync<RecognitionTypes>("dbo.procRecognitionTypesDetailById", new { ID = id }, commandType: CommandType.StoredProcedure);

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
                        //string strQuery = "INSERT INTO dbo.RecognitionTypes (Id,Name,Code)" +
                        //" VALUES (@Id,@Name,@Code);";

                        Guid? returnId = Guid.Empty;
                        if (_obj != null)
                        {
                            _obj.Id = Guid.NewGuid();

                            var _result = await _db.QueryFirstAsync<Guid>("dbo.procRecognitionTypesCreate", new
                            {
                                Id = _obj.Id,
                                Name = _obj.Name,
                                Code = _obj.Code
                            }, commandType: CommandType.StoredProcedure);

                            returnId = _result;
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
                        //string strQuery = "UPDATE dbo.RecognitionTypes SET " +
                        //"Name = @Name, " +
                        //"Code = @Code, " +
                        //"WHERE Id = @Id;";

                        Guid? returnId = Guid.Empty;
                        if (_obj != null)
                        {

                            var _result = await _db.ExecuteAsync("dbo.procRecognitionTypesUpdate", new
                            {
                                Id = _obj.Id,
                                Name = _obj.Name,
                                Code = _obj.Code
                                //CreatedBy = _obj.CreatedBy,
                                //DateCreated = _obj.DateCreated
                            }, commandType: CommandType.StoredProcedure);

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
                        //string strQuery = "DELETE FROM dbo.RecognitionTypes WHERE Id = @Id";

                        if (id != Guid.Empty)
                        {
                            var _result = await _db.ExecuteAsync("dbo.procRecognitionTypesDelete", new { Id = id }, commandType: CommandType.Text);
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
