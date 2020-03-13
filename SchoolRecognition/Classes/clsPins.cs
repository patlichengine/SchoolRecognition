using Dapper;
using SchoolRecognition.Models;
using SchoolRecognition.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Classes
{
    public class clsPins : IPins
    {
        public Task<List<Pins>> Get()
        {
            return Task.Run(async () =>
            {

                try
                {
                    using (IDbConnection _db = new clsDBConnection().OpenConnection())
                    {
                        var result = new List<Pins>();

                        string strQuery = "Select * from dbo.Pins;";

                        var _result = await _db.QueryAsync<Pins>(strQuery);

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
        public Task<Pins> Get(Guid id)
        {
            return Task.Run(async () =>
            {

                try
                {
                    using (IDbConnection _db = new clsDBConnection().OpenConnection())
                    {
                        Pins result = null;

                        if (id != Guid.Empty)
                        {
                            string strQuery = "Select * from dbo.Pins WHERE ID = @_id;";

                            var _result = await _db.QueryFirstOrDefaultAsync<Pins>(strQuery, new { _id = id });

                            result = _result;
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
        public Task<Guid?> Create(Pins _obj)
        {

            return Task.Run(async () =>
            {

                try
                {
                    using (IDbConnection _db = new clsDBConnection().OpenConnection())
                    {
                        string strQuery = "INSERT INTO dbo.Pins (ID,RecognitionTypeID,SerialPin,IsActive,IsInUse,CreatedBy,DateCreated)" +
                        " VALUES (@ID,@RecognitionTypeID,@SerialPin,@IsActive,@IsInUse,@CreatedBy,@DateCreated);";

                        Guid? returnId = Guid.Empty;
                        if (_obj != null)
                        {
                            _obj.Id = Guid.NewGuid();
                            _obj.DateCreated = DateTime.Now;

                            var _result = await _db.ExecuteAsync(strQuery, new 
                            { 
                                ID = _obj.Id,
                                RecognitionID = _obj.RecognitionTypeId,
                                SerialPin = _obj.SerialPin,
                                IsActive = _obj.IsActive,
                                IsInUse = _obj.IsInUse,
                                CreatedBy = _obj.CreatedBy,
                                DateCreated = _obj.DateCreated
                            }, commandType: CommandType.StoredProcedure);

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
        public Task<Pins> Update(Pins _obj)
        {

            return Task.Run(async () =>
            {

                try
                {
                    using (IDbConnection _db = new clsDBConnection().OpenConnection())
                    {
                        string strQuery = "UPDATE INTO dbo.Pins SET " + 
                        "RecognitionTypeID = @RecognitionTypeID, " + 
                        "SerialPin = @SerialPin, " +
                        "IsActive = @IsActive, " + 
                        "IsInUse = @IsInUse " + 
                        "WHERE ID = @ID;";

                        Guid? returnId = Guid.Empty;
                        if (_obj != null)
                        {
                            _obj.Id = Guid.NewGuid();
                            _obj.DateCreated = DateTime.Now;

                            var _result = await _db.ExecuteAsync(strQuery, new
                            {
                                ID = _obj.Id,
                                RecognitionID = _obj.RecognitionTypeId,
                                SerialPin = _obj.SerialPin,
                                IsActive = _obj.IsActive,
                                IsInUse = _obj.IsInUse,
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
        public Task Delete(Guid pinId) //return type is void
        {

            return Task.Run(async () =>
            {

                try
                {
                    using (IDbConnection _db = new clsDBConnection().OpenConnection())
                    {
                        string strQuery = "DELETE FROM dbo.Pins WHERE ID = @ID";

                        if (pinId != Guid.Empty)
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
