using Dapper;
using Microsoft.Data.SqlClient;
using SchoolRecognition.Classes;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public class cPinsRepository : IPins
    {
        private readonly ConnectionString _connectionString;
        public cPinsRepository(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        private const string WAECCODEPREFIX = "WC";
        //private const string CHARS = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public Task<List<Pins>> Get()
        {
            return Task.Run(async () =>
            {

                try
                {
                    using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                    {
                        var result = new List<Pins>();

                        //string strQuery = "Select * from dbo.PINs;";
                        //string strQuery = "SELECT * FROM dbo.PINs AS pin " +
                        //"LEFT JOIN dbo.Users AS usr ON pin.CreatedBy = usr.Id " +
                        //"LEFT JOIN dbo.RecognitionTypes AS rType ON pin.RecognitionTypeId = rType.Id;";


                        var _result = await _db.QueryAsync<Pins, Users, RecognitionTypes, Pins>(
                            "dbo.procPinsList", 
                            (pin, user, recognitionType) => {

                                if (pin != null)
                                {
                                    pin.CreatedByNavigation = user;
                                    pin.RecognitionType = recognitionType;
                                }

                                return pin;

                            },
                            splitOn: "Id", commandType: CommandType.StoredProcedure);

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
        public Task<List<Pins>> GetPinsByRecognitionTypeId(Guid recognitionTypeId)
        {
            return Task.Run(async () =>
            {

                try
                {
                    using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                    {
                        var result = new List<Pins>();

                        if (recognitionTypeId != Guid.Empty)
                        {
                            //string strQuery = "Select * from dbo.PINs;";
                            //string strQuery = "SELECT * FROM dbo.PINs AS pin " +
                            //"LEFT JOIN dbo.Users AS usr ON pin.CreatedBy = usr.Id " +
                            //"LEFT JOIN dbo.RecognitionTypes AS rType ON pin.RecognitionTypeId = rType.Id " +
                            //"WHERE pin.RecognitionTypeId = @_recognitionTypeId;";

                            var _result = await _db.QueryAsync<Pins, Users, RecognitionTypes, Pins>(
                                "dbo.procPinsListByRecognitionTypeId",
                                (pin, user, recognitionType) =>
                                {

                                    if (pin != null)
                                    {
                                        pin.CreatedByNavigation = user;
                                        pin.RecognitionType = recognitionType;
                                    }

                                    return pin;

                                }, new { RecognitionTypeID = recognitionTypeId },
                                splitOn: "Id", commandType: CommandType.StoredProcedure);

                            result = _result.ToList();
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
        Task<string> GenerateSerialPin(Guid recognitionTypeId)
        {
            return Task.Run(async () =>
            {

                try
                {

                    using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                    {
                        //
                        string strGeneratedSerialPin = null;
                        //The strings below will combine with the TotalNumberOfPins to give the final generated pin
                        string strRecogntionTypeCode = null;
                        string strAlphanumericSecurityCode = null;
                        //
                        int intTotalNumberOfPins = 0;


                        if (recognitionTypeId != Guid.Empty)
                        {
                            string strQueryRecognitionTypes = "Select * from dbo.RecognitionTypes WHERE Id = @_id;";
                            string strQueryPinSerialPins = "Select SerialPin from dbo.PINs WHERE RecognitionTypeId = @_recognitionTypeId;";

                            //Get List of RecognitionTypes
                            var _recognitionType = await _db.QueryFirstOrDefaultAsync<RecognitionTypes>(strQueryRecognitionTypes, new { _id = recognitionTypeId });
                            //Get List of Pins of the given RecognitionType currently in the DB
                            var _pinSerialPins = await _db.QueryAsync<string>(strQueryPinSerialPins, new { _recognitionTypeId = recognitionTypeId });

                            if (_recognitionType != null)
                            {
                                //Resolve RecognitionTypes Code
                                strRecogntionTypeCode = _recognitionType.Code;
                                //Resolve Current Total Number of Pins in DB
                                intTotalNumberOfPins = _pinSerialPins.ToList().Count;
                                //Increment TotalNumber of PINs
                                intTotalNumberOfPins++;
                                //Generate Random 3 character Alphanumeric suffix
                                var guid = Guid.NewGuid();
                                strAlphanumericSecurityCode = guid.ToString().Substring(0, 3);
                                //Combine components of the GeneratedSerialPin
                                strGeneratedSerialPin = String.Format("{0}{1}{2,5:00000}{3}", 
                                    WAECCODEPREFIX, strRecogntionTypeCode, intTotalNumberOfPins, strAlphanumericSecurityCode.ToUpper());
                                
                            }
                        }

                        return strGeneratedSerialPin;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            });
        }
        Task<List<string>> GenerateSerialPins(Guid recognitionTypeId, int numberOfPins)
        {
            return Task.Run(async () =>
            {

                try
                {

                    using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                    {
                        //
                        List<string> strGeneratedSerialPins = new List<string>();
                        //The strings below will combine with the TotalNumberOfPins to give the final generated pin
                        string strRecogntionTypeCode = null;
                        string strAlphanumericSecurityCode = null;
                        //
                        int intTotalNumberOfPins = 0;


                        if (recognitionTypeId != Guid.Empty)
                        {
                            string strQueryRecognitionTypes = "Select * from dbo.RecognitionTypes WHERE Id = @_id;";
                            string strQueryPinSerialPins = "Select SerialPin from dbo.PINs WHERE RecognitionTypeId = @_recognitionTypeId;";

                            //Get List of RecognitionTypes
                            var _recognitionType = await _db.QueryFirstOrDefaultAsync<RecognitionTypes>(strQueryRecognitionTypes, new { _id = recognitionTypeId });
                            //Get List of Pins of the given RecognitionType currently in the DB
                            var _pinSerialPins = await _db.QueryAsync<string>(strQueryPinSerialPins, new { _recognitionTypeId = recognitionTypeId });

                            if (_recognitionType != null)
                            {

                                //Resolve RecognitionTypes Code
                                strRecogntionTypeCode = _recognitionType.Code;
                                //Resolve Current Total Number of Pins in DB
                                intTotalNumberOfPins = _pinSerialPins.ToList().Count;

                                for (int i = 0; i < numberOfPins; i++)
                                {
                                    //Increment TotalNumber of PINs
                                    intTotalNumberOfPins++;
                                    //Generate Random 3 character Alphanumeric suffix
                                    var guid = Guid.NewGuid();
                                    strAlphanumericSecurityCode = guid.ToString().Substring(0, 3);
                                    //Combine components of the GeneratedSerialPin
                                    string _strGeneratedSerialPin = String.Format("{0}{1}{2,5:00000}{3}",
                                        WAECCODEPREFIX, strRecogntionTypeCode, intTotalNumberOfPins, strAlphanumericSecurityCode.ToUpper());

                                    //Add to list of strings
                                    strGeneratedSerialPins.Add(_strGeneratedSerialPin);
                                }

                            }
                        }

                        return strGeneratedSerialPins;
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
                    using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                    {
                        Pins result = null;

                        if (id != Guid.Empty)
                        {
                            //string strQuery = "Select * from dbo.PINs WHERE ID = @_id;";
                            //string strQuery = "SELECT * FROM dbo.PINs AS pin " +
                            //    "LEFT JOIN dbo.Users AS usr ON pin.CreatedBy = usr.Id " +
                            //    "LEFT JOIN dbo.RecognitionTypes AS rType ON pin.RecognitionTypeId = rType.Id " +
                            //    "LEFT JOIN dbo.PinHistories AS pnHstrys ON pin.Id = pnHstrys.PinId " +
                            //    "LEFT JOIN dbo.SchoolPayments AS schPays ON pin.Id = schPays.PinId " +
                            //    "WHERE pin.Id = @ID;";

                            var pinsDictionary = new Dictionary<Guid, Pins>();

                            var _result = await _db.QueryAsync<Pins, Users, RecognitionTypes, PinHistories, SchoolPayments, Pins>(
                                "dbo.procPinsDetailById",
                                (pin, user, recognitionType, listPinHistorys, listSchoolPayments) =>
                                {
                                    Pins pinData;

                                    if (!pinsDictionary.TryGetValue(pin.Id, out pinData))
                                    {
                                        pinData = pin;
                                        pinData.CreatedByNavigation = user;
                                        pinData.RecognitionType = recognitionType;
                                        //
                                        pinData.PinHistories = new List<PinHistories>();
                                        pinData.SchoolPayments = new List<SchoolPayments>();
                                        //
                                        pinsDictionary.Add(pinData.Id, pinData);
                                    }

                                    pinData.PinHistories.Add(listPinHistorys);
                                    pinData.SchoolPayments.Add(listSchoolPayments);

                                    return pinData;

                                }, new { ID = id },
                                splitOn: "Id", commandType: CommandType.StoredProcedure);

                            if (_result != null)
                            {
                                result = _result.FirstOrDefault();
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
        public Task<Guid?> Create(Pins _obj)
        {

            return Task.Run(async () =>
            {

                try
                {
                    using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                    {
                        //string strQuery = "INSERT INTO dbo.PINs (Id,RecognitionTypeId,SerialPin,IsActive,IsInUse,CreatedBy,DateCreated)" +
                        //" VALUES (@Id,@RecognitionTypeId,@SerialPin,@IsActive,@IsInUse,@CreatedBy,@DateCreated);";

                        Guid? returnId = Guid.Empty;
                        if (_obj != null)
                        {
                            _obj.Id = Guid.NewGuid();
                            _obj.DateCreated = DateTime.Now;
                            //Generate a Custom SerialPin
                            if (_obj.RecognitionTypeId != null && _obj.RecognitionTypeId != Guid.Empty)
                            {
                                _obj.SerialPin = await GenerateSerialPin(_obj.RecognitionTypeId.Value);
                            }

                            var _result = await _db.ExecuteAsync("dbo.procPinsCreate", new 
                            {
                                Id = _obj.Id,
                                RecognitionId = _obj.RecognitionTypeId,
                                SerialPin = _obj.SerialPin,
                                //IsActive = _obj.IsActive,
                                //IsInUse = _obj.IsInUse,
                                CreatedBy = _obj.CreatedBy,
                                //DateCreated = _obj.DateCreated
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
        public Task<bool> CreateSeveralPins(Pins _obj, int numberOfPinsToGenerate)
        {

            return Task.Run(async () =>
            {

                try
                {
                    using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                    {

                        List<string> strGeneratedSerialPins = new List<string>();

                        List<Pins> listObjPins = new List<Pins>();

                        //string strQuery = "INSERT INTO dbo.PINs (Id, RecognitionTypeId, SerialPin, IsActive, IsInUse, CreatedBy, DateCreated)" +
                        //" VALUES (@Id, @RecognitionTypeId, @SerialPin, @IsActive, @IsInUse, @CreatedBy, @DateCreated);";

                        Guid? returnId = Guid.Empty;
                        if (_obj != null)
                        {
                            //Generate a List of Custom SerialPin
                            if (_obj.RecognitionTypeId != null && _obj.RecognitionTypeId != Guid.Empty)
                            {
                                strGeneratedSerialPins = await GenerateSerialPins(_obj.RecognitionTypeId.Value, numberOfPinsToGenerate);
                            }

                            //Assign Custom SerialPins to Pin objects
                            for (int i = 0; i < numberOfPinsToGenerate; i++)
                            {
                                //Get string at position "i" in the string array
                                string strGeneratedSerialPin = strGeneratedSerialPins[i];

                                Pins pinObj = new Pins()
                                {
                                    //Id = Guid.NewGuid(),
                                    //
                                    RecognitionTypeId = _obj.RecognitionTypeId,
                                    SerialPin = strGeneratedSerialPin,
                                    //IsActive = _obj.IsActive,
                                    //IsInUse = _obj.IsInUse,
                                    CreatedBy = _obj.CreatedBy,
                                    //DateCreated = DateTime.Now,
                                };

                                listObjPins.Add(pinObj);
                            }

                            var parameters = listObjPins.Select(x =>
                            {
                                var tempParams = new DynamicParameters();
                                tempParams.Add("@RecognitionTypeID", x.RecognitionTypeId, DbType.Guid, ParameterDirection.Input);
                                tempParams.Add("@SerialPin", x.SerialPin, DbType.String, ParameterDirection.Input);
                                tempParams.Add("@CreatedBy", x.CreatedBy, DbType.Guid, ParameterDirection.Input);
                                return tempParams;
                            });

                            var _result = await _db.ExecuteAsync("dbo.procPinsCreate", parameters, commandType: CommandType.StoredProcedure);

                            return true;

                        }

                        return false;
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
                    using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                    {
                        //string strQuery = "UPDATE INTO dbo.PINs SET " + 
                        //"RecognitionTypeId = @RecognitionTypeId, " + 
                        //"SerialPin = @SerialPin, " +
                        //"IsActive = @IsActive, " + 
                        //"IsInUse = @IsInUse " + 
                        //"WHERE Id = @Id;";

                        Guid? returnId = Guid.Empty;
                        if (_obj != null)
                        {
                            _obj.Id = Guid.NewGuid();
                            _obj.DateCreated = DateTime.Now;

                            var _result = await _db.ExecuteAsync("dbo.procPinsUpdate", new
                            {
                                Id = _obj.Id,
                                RecognitionId = _obj.RecognitionTypeId,
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
        public Task Delete(Guid id) //return type is void
        {

            return Task.Run(async () =>
            {

                try
                {
                    using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                    {
                        //string strQuery = "DELETE FROM dbo.PINs WHERE Id = @Id";

                        if (id != Guid.Empty)
                        {
                            var _result = await _db.ExecuteAsync("dbo.procPinsDelete", commandType: CommandType.StoredProcedure);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            });
        }


        //Please use the method below as an example on how to write
        // the above methods in synchronous mode
        //public List<Pins> Get()
        //{
        //    try
        //    {
        //        using (IDbConnection _db = new SqlConnection(_connectionString.Value))
        //        {
        //            var result = new List<Pins>();

        //            string strQuery = "Select * from dbo.PINs;";

        //            var _result = _db.Query<Pins>(strQuery);

        //            result = _result.ToList();

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
    }
}
