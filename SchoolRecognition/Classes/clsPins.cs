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

        private const string WAECCODEPREFIX = "WC";
        //private const string CHARS = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

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
        Task<string> GenerateSerialPin(Guid recognitionTypeID)
        {
            return Task.Run(async () =>
            {

                try
                {

                    using (IDbConnection _db = new clsDBConnection().OpenConnection())
                    {
                        //
                        string strGeneratedSerialPin = null;
                        //The strings below will combine with the TotalNumberOfPins to give the final generated pin
                        string strRecogntionTypeCode = null;
                        string strAlphanumericSecurityCode = null;
                        //
                        int intTotalNumberOfPins = 0;


                        if (recognitionTypeID != Guid.Empty)
                        {
                            string strQueryRecognitionTypes = "Select * from dbo.RecognitionTypes WHERE ID = @_id;";
                            string strQueryPinSerialPins = "Select SerialPin from dbo.Pins;";

                            //Get List of RecognitionTypes
                            var _recognitionType = await _db.QueryFirstOrDefaultAsync<RecognitionTypes>(strQueryRecognitionTypes, new { _id = recognitionTypeID });
                            //Get List of Pins Currently
                            var _pinSerialPins = await _db.QueryFirstOrDefaultAsync<string>(strQueryPinSerialPins, new { _id = recognitionTypeID });

                            if (_recognitionType != null)
                            {
                                //Resolve RecognitionTypes Code
                                strRecogntionTypeCode = _recognitionType.Code;
                                //Resolve Current Total Number of Pins in DB
                                intTotalNumberOfPins = _pinSerialPins.ToList().Count;
                                //Generate Random 3 character Alphanumeric suffix
                                var guid = Guid.NewGuid();
                                strAlphanumericSecurityCode = guid.ToString().Substring(0, 3);
                                //Combine components of the GeneratedSerialPin
                                strGeneratedSerialPin = String.Format("{0}{1}{2,5:00000}{3}", 
                                    WAECCODEPREFIX, strRecogntionTypeCode, intTotalNumberOfPins, strAlphanumericSecurityCode);
                                
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
        Task<List<string>> GenerateSerialPins(Guid recognitionTypeID, int numberOfPins)
        {
            return Task.Run(async () =>
            {

                try
                {

                    using (IDbConnection _db = new clsDBConnection().OpenConnection())
                    {
                        //
                        List<string> strGeneratedSerialPins = new List<string>();
                        //The strings below will combine with the TotalNumberOfPins to give the final generated pin
                        string strRecogntionTypeCode = null;
                        string strAlphanumericSecurityCode = null;
                        //
                        int intTotalNumberOfPins = 0;


                        if (recognitionTypeID != Guid.Empty)
                        {
                            string strQueryRecognitionTypes = "Select * from dbo.RecognitionTypes WHERE ID = @_id;";
                            string strQueryPinSerialPins = "Select SerialPin from dbo.Pins;";

                            //Get List of RecognitionTypes
                            var _recognitionType = await _db.QueryFirstOrDefaultAsync<RecognitionTypes>(strQueryRecognitionTypes, new { _id = recognitionTypeID });
                            //Get List of Pins Currently
                            var _pinSerialPins = await _db.QueryFirstOrDefaultAsync<string>(strQueryPinSerialPins, new { _id = recognitionTypeID });

                            if (_recognitionType != null)
                            {
                                for (int i = 0; i < numberOfPins; i++)
                                {
                                    //Resolve RecognitionTypes Code
                                    strRecogntionTypeCode = _recognitionType.Code;
                                    //Resolve Current Total Number of Pins in DB
                                    intTotalNumberOfPins = _pinSerialPins.ToList().Count;
                                    //Generate Random 3 character Alphanumeric suffix
                                    var guid = Guid.NewGuid();
                                    strAlphanumericSecurityCode = guid.ToString().Substring(0, 3);
                                    //Combine components of the GeneratedSerialPin
                                    string _strGeneratedSerialPin = String.Format("{0}{1}{2,5:00000}{3}",
                                        WAECCODEPREFIX, strRecogntionTypeCode, intTotalNumberOfPins, strAlphanumericSecurityCode);

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
                            //Generate a Custom SerialPin
                            if (_obj.RecognitionTypeId != Guid.Empty)
                            {
                                _obj.SerialPin = await GenerateSerialPin(_obj.RecognitionTypeId.Value);
                            }

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
        public Task<bool> CreateSeveralPins(Pins _obj, int numberOfPinsToGenerate)
        {

            return Task.Run(async () =>
            {

                try
                {
                    using (IDbConnection _db = new clsDBConnection().OpenConnection())
                    {

                        List<string> strGeneratedSerialPins = new List<string>();
                        List<Pins> listObjPins = new List<Pins>();

                        string strQuery = "INSERT INTO dbo.Pins (ID,RecognitionTypeID,SerialPin,IsActive,IsInUse,CreatedBy,DateCreated)" +
                        " VALUES (@ID,@RecognitionTypeID,@SerialPin,@IsActive,@IsInUse,@CreatedBy,@DateCreated);";

                        Guid? returnId = Guid.Empty;
                        if (_obj != null)
                        {
                            //Generate a List of Custom SerialPin
                            if (_obj.RecognitionType != null && _obj.RecognitionTypeId != Guid.Empty)
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
                                    Id = Guid.NewGuid(),
                                    DateCreated = DateTime.Now,
                                    //
                                    SerialPin = strGeneratedSerialPin,
                                    IsActive = _obj.IsActive,
                                    IsInUse = _obj.IsInUse,
                                    CreatedBy = _obj.CreatedBy,
                                };
                            }

                            var _result = await _db.ExecuteAsync(strQuery, 
                                from pinObject in listObjPins
                                        select new                                                                           
                                        {
                                            ID = pinObject.Id,
                                            RecognitionID = pinObject.RecognitionTypeId,
                                            SerialPin = pinObject.SerialPin,
                                            IsActive = pinObject.IsActive,
                                            IsInUse = pinObject.IsInUse,
                                            CreatedBy = pinObject.CreatedBy,
                                            DateCreated = pinObject.DateCreated
                                        }, commandType: CommandType.StoredProcedure);

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


        //Please use the method below as an example on how to write
        // the above methods in synchronous mode
        //public List<Pins> Get()
        //{
        //    try
        //    {
        //        using (IDbConnection _db = new clsDBConnection().OpenConnection())
        //        {
        //            var result = new List<Pins>();

        //            string strQuery = "Select * from dbo.Pins;";

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
