using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.Classes;
using SchoolRecognition.DbContexts;
using SchoolRecognition.Entities;
using SchoolRecognition.Extensions;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public class cPinsRepository : IPinsRepository, IDisposable
    {
        //private readonly ConnectionString _connectionString;
        //public cPinsRepository(ConnectionString connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;

        //public cRecognitionTypesRepository(ConnectionString connectionString)
        //{
        //    //_connectionString = connectionString;
        //    //_pinService = new cPinsRepository(_connectionString);

        //}

        public cPinsRepository(SchoolRecognitionContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        private const string WAECCODEPREFIX = "WC";
        //private const string CHARS = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public  async Task<IEnumerable<PinsViewDto>> Get()
        {
            try
            {
                var _result = await _context.Pins
                    .Include(x => x.RecognitionType)
                    .Include(x => x.CreatedByNavigation)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<PinsViewDto>>(_result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<CustomPagedList<PinsViewDto>> Get(int? rangeIndex)
        {
            //Default Range Limit is 100 Rows
            int _lowerLimit = 0;
            int _upperLimit = 100;
            try
            {
                //Instantiate Array objects
                IList<PinsViewDto> listOfDtos = new List<PinsViewDto>();
                var cstPageList = new CustomPagedList<PinsViewDto>();

                var count = await _context.Pins.CountAsync();

                //Set Range of Row Based on rangeIndex parameter
                if (rangeIndex > 0)
                {
                    _lowerLimit = (rangeIndex.Value) * 100;
                    _upperLimit = (rangeIndex.Value + 1) * 100;
                }

                var _result = await _context.Pins
                    .Include(x => x.RecognitionType)
                    .Include(x => x.CreatedByNavigation)
                    .Skip(_lowerLimit)
                    .Take((_upperLimit - _lowerLimit))
                    .ToListAsync();
                //Assign count value
                cstPageList.TotalDBEntitysCount = count;
                //Map list of entities to list of dtos
                listOfDtos = _mapper.Map<IList<PinsViewDto>>(_result);
                //Assign list value
                cstPageList.Entitys = listOfDtos.ToList();
                //Return value of upper and lower limit
                cstPageList.LowerLimit = _lowerLimit;
                cstPageList.UpperLimit= _upperLimit;

                return cstPageList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<CustomPagedList<PinsViewDto>> Get(int? rangeIndex, string searchQuery)
        {
            //Default Range Limit is 100 Rows
            int _lowerLimit = 0;
            int _upperLimit = 100;
            //Default search query
            string _searchQuery = "";
            try
            {
                //Instantiate Array objects
                IList<PinsViewDto> listOfDtos = new List<PinsViewDto>();
                var cstPageList = new CustomPagedList<PinsViewDto>();

                var count = await _context.Pins.CountAsync();

                //Set Range of Row Based on rangeIndex parameter
                if (rangeIndex > 0)
                {
                    _lowerLimit = (rangeIndex.Value) * 100;
                    _upperLimit = (rangeIndex.Value + 1) * 100;
                }

                //Check searchQuery paramter is not null
                if (searchQuery != null)
                {
                    _searchQuery = searchQuery;
                }

                var _result = await _context.Pins
                    .Include(x => x.RecognitionType)
                    .Include(x => x.CreatedByNavigation)
                    .Where(
                    //Add all columns you wish to search
                    x => x.SerialPin.ToUpper().Contains(_searchQuery.ToUpper())
                    || (x.DateCreated != null ? x.DateCreated.Value.ToString().ToUpper().Contains(_searchQuery.ToUpper()) : false)
                    || (x.RecognitionType != null ? x.RecognitionType.Code.ToUpper().Contains(_searchQuery.ToUpper()) : false)
                    || (x.CreatedByNavigation != null ? x.CreatedByNavigation.Surname.ToUpper().Contains(_searchQuery.ToUpper()) : false)
                    || (x.CreatedByNavigation != null ? x.CreatedByNavigation.Othernames.ToUpper().Contains(_searchQuery.ToUpper()) : false)
                    )
                    .Skip(_lowerLimit)
                    .Take((_upperLimit - _lowerLimit))
                    .ToListAsync();
                //Assign count value
                cstPageList.TotalDBEntitysCount = count;
                //Map list of entities to list of dtos
                listOfDtos = _mapper.Map<IList<PinsViewDto>>(_result);
                //Assign list value
                cstPageList.Entitys = listOfDtos.ToList();
                //Return value of upper and lower limit
                cstPageList.LowerLimit = _lowerLimit;
                cstPageList.UpperLimit= _upperLimit;

                return cstPageList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<CustomPagedList<PinsViewDto>> Get(int? rangeIndex, string searchQuery, string orderCriteria, bool reverseOrder)
        {
            //Default Range Limit is 100 Rows
            int _lowerLimit = 0;
            int _upperLimit = 100;
            //Default search query
            string _searchQuery = "";
            try
            {
                //Instantiate Array objects
                IList<PinsViewDto> listOfDtos = new List<PinsViewDto>();
                IList<Pins> listResult = new List<Pins>();
                var cstPageList = new CustomPagedList<PinsViewDto>();

                var count = await _context.Pins.CountAsync();

                //Set Range of Row Based on rangeIndex parameter
                if (rangeIndex > 0)
                {
                    _lowerLimit = (rangeIndex.Value) * 100;
                    _upperLimit = (rangeIndex.Value + 1) * 100;
                }

                //Check searchQuery paramter is not null
                if (searchQuery != null)
                {
                    _searchQuery = searchQuery;
                }

                //Enabling orderCriteria 
                var orderParameter = typeof(Pins).GetProperty(orderCriteria);

                ///Order or reverse order  by a property
                if (reverseOrder == false)
                {
                    var _result = await _context.Pins
                    .Include(x => x.RecognitionType)
                    .Include(x => x.CreatedByNavigation)
                    .Where(
                    //Add all columns you wish to search
                    x => x.SerialPin.ToUpper().Contains(_searchQuery.ToUpper())
                    || (x.DateCreated != null ? x.DateCreated.Value.ToString().ToUpper().Contains(_searchQuery.ToUpper()) : false)
                    || (x.RecognitionType != null ? x.RecognitionType.Code.ToUpper().Contains(_searchQuery.ToUpper()) : false)
                    || (x.CreatedByNavigation != null ? x.CreatedByNavigation.Surname.ToUpper().Contains(_searchQuery.ToUpper()) : false)
                    || (x.CreatedByNavigation != null ? x.CreatedByNavigation.Othernames.ToUpper().Contains(_searchQuery.ToUpper()) : false)
                    )
                    .OrderBy(x => orderParameter.Name)
                    .Skip(_lowerLimit)
                    .Take((_upperLimit - _lowerLimit))
                    .ToListAsync();

                    listResult = _result;
                }
                else
                {
                    var _result = await _context.Pins
                    .Include(x => x.RecognitionType)
                    .Include(x => x.CreatedByNavigation)
                    .Where(
                    //Add all columns you wish to search
                    x => x.SerialPin.ToUpper().Contains(_searchQuery.ToUpper())
                    || (x.DateCreated != null ? x.DateCreated.Value.ToString().ToUpper().Contains(_searchQuery.ToUpper()) : false)
                    || (x.RecognitionType != null ? x.RecognitionType.Code.ToUpper().Contains(_searchQuery.ToUpper()) : false)
                    || (x.CreatedByNavigation != null ? x.CreatedByNavigation.Surname.ToUpper().Contains(_searchQuery.ToUpper()) : false)
                    || (x.CreatedByNavigation != null ? x.CreatedByNavigation.Othernames.ToUpper().Contains(_searchQuery.ToUpper()) : false)
                    )
                    //Reverse ordering
                    .OrderByDescending(x => orderParameter.Name)
                    .Skip(_lowerLimit)
                    .Take((_upperLimit - _lowerLimit))
                    .ToListAsync();
                    //
                    listResult = _result;
                }
                //Assign count value
                cstPageList.TotalDBEntitysCount = count;
                //Map list of entities to list of dtos
                listOfDtos = _mapper.Map<IList<PinsViewDto>>(listResult);
                //Assign list value
                cstPageList.Entitys = listOfDtos.ToList();
                //Return value of upper and lower limit
                cstPageList.LowerLimit = _lowerLimit;
                cstPageList.UpperLimit= _upperLimit;

                return cstPageList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<PinsViewDto>> GetPinsByRecognitionTypeId(Guid recognitionTypeId)
        {
            try
            {

                var result = new List<PinsViewDto>();
                if (recognitionTypeId != Guid.Empty)
                {
                    var _result = await _context.Pins.Include(x => x.RecognitionType).Include(x => x.CreatedByNavigation).Where(x => x.RecognitionTypeId == recognitionTypeId).ToListAsync();

                    result = _mapper.Map<IEnumerable<PinsViewDto>>(_result).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        async Task <string> GenerateSerialPin(Guid recognitionTypeId)
        {
            try
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

                    //Get RecognitionType
                    var _recognitionType = await _context.RecognitionTypes.Include(x => x.Pins).Where(x => x.Id == recognitionTypeId).FirstOrDefaultAsync();
                    //Get List of Pins of the given RecognitionType currently in the DB

                    if (_recognitionType != null)
                    {
                        //Resolve RecognitionTypes Code
                        strRecogntionTypeCode = _recognitionType.Code;
                        //Resolve Current Total Number of Pins in DB
                        intTotalNumberOfPins = _recognitionType.Pins.ToList().Count;
                        //Increment TotalNumber of PINs
                        intTotalNumberOfPins++;
                        //Generate Random 3 character Alphanumeric suffix
                        var guid = Guid.NewGuid();
                        strAlphanumericSecurityCode = guid.ToString().Substring(0, 3);
                        //Combine components of the GeneratedSerialPin
                        strGeneratedSerialPin = String.Format("{0}{1}{2,5:00000}{3}",
                            WAECCODEPREFIX, strRecogntionTypeCode, intTotalNumberOfPins, strAlphanumericSecurityCode);

                    }
                }

                return strGeneratedSerialPin.ToUpper();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        async Task<List<string>> GenerateSerialPins(Guid recognitionTypeId, int numberOfPins)
        {
            try
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
                    //Get RecognitionType
                    var _recognitionType = await _context.RecognitionTypes.Include(x => x.Pins).Where(x => x.Id == recognitionTypeId).FirstOrDefaultAsync();
                    //Get List of Pins of the given RecognitionType currently in the DB

                    if (_recognitionType != null)
                    {

                        //Resolve RecognitionTypes Code
                        strRecogntionTypeCode = _recognitionType.Code;
                        //Resolve Current Total Number of Pins in DB
                        intTotalNumberOfPins = _recognitionType.Pins.ToList().Count;

                        for (int i = 0; i < numberOfPins; i++)
                        {
                            //Increment TotalNumber of PINs
                            intTotalNumberOfPins++;
                            //Generate Random 3 character Alphanumeric suffix
                            var guid = Guid.NewGuid();
                            strAlphanumericSecurityCode = guid.ToString().Substring(0, 3);
                            //Combine components of the GeneratedSerialPin
                            string _strGeneratedSerialPin = String.Format("{0}{1}{2,5:00000}{3}",
                                WAECCODEPREFIX, strRecogntionTypeCode, intTotalNumberOfPins, strAlphanumericSecurityCode);

                            //Add to list of strings
                            strGeneratedSerialPins.Add(_strGeneratedSerialPin.ToUpper());
                        }

                    }
                }

                return strGeneratedSerialPins;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<PinsViewDto> Get(Guid id)
        {
            try
            {
                PinsViewDto result = null;

                if (id != Guid.Empty)
                {

                    var _result = await _context.Pins
                        .Include(x => x.CreatedByNavigation)
                        .ThenInclude(r => r.Role)
                        .Include(y => y.RecognitionType)
                        .Include(z => z.SchoolPayments)
                        .FirstOrDefaultAsync();
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Guid?> Create(PinsCreateDto _obj)
        {

            try
            {
                //ID Expected as the return type of the method
                Guid? returnId = null;

                if (_obj != null)
                {
                    //Variable to store the SerialPin to be generated
                    string serialPin = null;

                    if (_obj.RecognitionTypeId != Guid.Empty)
                    {
                        //Generate a SerialPin
                        serialPin = await GenerateSerialPin(_obj.RecognitionTypeId);
                    }
                    //Create a PINs Object to be stored in the db
                    var entity = new Pins()
                    {
                        Id = Guid.NewGuid(),
                        RecognitionTypeId = _obj.RecognitionTypeId,
                        IsActive = _obj.IsActive,
                        CreatedBy = _obj.CreatedBy,
                        DateCreated = DateTime.Now
                    };

                    returnId = entity.Id;

                    await _context.Pins.AddAsync(entity);
                    await this.Save();

                }
                return returnId;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public Task<bool> CreateSeveralPins(Pins _obj, int numberOfPinsToGenerate)
        //{

        //    return Task.Run(async () =>
        //    {

        //        try
        //        {
        //            using (IDbConnection _db = new SqlConnection(_connectionString.Value))
        //            {

        //                List<string> strGeneratedSerialPins = new List<string>();

        //                List<Pins> listObjPins = new List<Pins>();

        //                //string strQuery = "INSERT INTO dbo.PINs (Id, RecognitionTypeId, SerialPin, IsActive, IsInUse, CreatedBy, DateCreated)" +
        //                //" VALUES (@Id, @RecognitionTypeId, @SerialPin, @IsActive, @IsInUse, @CreatedBy, @DateCreated);";

        //                Guid? returnId = Guid.Empty;
        //                if (_obj != null)
        //                {
        //                    //Generate a List of Custom SerialPin
        //                    if (_obj.RecognitionTypeId != null && _obj.RecognitionTypeId != Guid.Empty)
        //                    {
        //                        strGeneratedSerialPins = await GenerateSerialPins(_obj.RecognitionTypeId.Value, numberOfPinsToGenerate);
        //                    }

        //                    //Assign Custom SerialPins to Pin objects
        //                    for (int i = 0; i < numberOfPinsToGenerate; i++)
        //                    {
        //                        //Get string at position "i" in the string array
        //                        string strGeneratedSerialPin = strGeneratedSerialPins[i];

        //                        Pins pinObj = new Pins()
        //                        {
        //                            //Id = Guid.NewGuid(),
        //                            //
        //                            RecognitionTypeId = _obj.RecognitionTypeId,
        //                            SerialPin = strGeneratedSerialPin,
        //                            //IsActive = _obj.IsActive,
        //                            //IsInUse = _obj.IsInUse,
        //                            CreatedBy = _obj.CreatedBy,
        //                            //DateCreated = DateTime.Now,
        //                        };

        //                        listObjPins.Add(pinObj);
        //                    }

        //                    var parameters = listObjPins.Select(x =>
        //                    {
        //                        var tempParams = new DynamicParameters();
        //                        tempParams.Add("@RecognitionTypeID", x.RecognitionTypeId, DbType.Guid, ParameterDirection.Input);
        //                        tempParams.Add("@SerialPin", x.SerialPin, DbType.String, ParameterDirection.Input);
        //                        tempParams.Add("@CreatedBy", x.CreatedBy, DbType.Guid, ParameterDirection.Input);
        //                        return tempParams;
        //                    });

        //                    var _result = await _db.ExecuteAsync("dbo.procPinsCreate", parameters, commandType: CommandType.StoredProcedure);

        //                    return true;

        //                }

        //                return false;
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }

        //    });
        //}

        public async Task<bool> CreateSeveralPins(PinsCreateDto _obj)
        {

            try
            {
                List<string> strGeneratedSerialPins = new List<string>();

                List<Pins> listPinsEntities = new List<Pins>();

                int numberOfPinsToGenerate = _obj.NoOfPinToGenerate;

                Guid? returnId = Guid.Empty;
                if (_obj != null)
                {
                    //Generate a List of Custom SerialPin
                    if (_obj.RecognitionTypeId != null && _obj.RecognitionTypeId != Guid.Empty)
                    {
                        strGeneratedSerialPins = await GenerateSerialPins(_obj.RecognitionTypeId, numberOfPinsToGenerate);
                    }

                    //Assign Custom SerialPins to Pin objects
                    for (int i = 0; i < numberOfPinsToGenerate; i++)
                    {
                        //Get string at position "i" in the string array
                        string strGeneratedSerialPin = strGeneratedSerialPins[i];

                        Pins pinsEntity = new Pins()
                        {
                            //Id = Guid.NewGuid(),
                            //
                            RecognitionTypeId = _obj.RecognitionTypeId,
                            SerialPin = strGeneratedSerialPin,
                            IsActive = _obj.IsActive,
                            //IsInUse = _obj.IsInUse,
                            CreatedBy = _obj.CreatedBy,
                            DateCreated = DateTime.Now,
                        };

                        listPinsEntities.Add(pinsEntity);
                    }

                    await _context.Pins.AddRangeAsync(listPinsEntities);
                    await this.Save();

                    return true;

                }

                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<PinsUpdateDto> Update(PinsUpdateDto _obj)
        {

            try
            {
                if (_obj != null && _obj.Id != Guid.Empty)
                {
                    Pins entity = _mapper.Map<Pins>(_obj);

                    _context.Pins.Update(entity);
                    await this.Save();
                }

                return _obj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task Delete(Guid id) //return type is void
        {

            try
            {
                //using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                //{
                //    //string strQuery = "DELETE FROM dbo.PINs WHERE Id = @Id";

                //    if (id != Guid.Empty)
                //    {
                //        var _result = await _db.ExecuteAsync("dbo.procPinsDelete", commandType: CommandType.StoredProcedure);
                //    }
                //}
                if (id != Guid.Empty)
                {
                    var entity = await _context.Pins.Where(x => x.Id == id).FirstOrDefaultAsync();
                    if (entity != null)
                    {
                        _context.Remove(entity);
                        await this.Save();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        #region Base Methods


        async Task<bool> Save()
        {
            return await Task.Run(async () => {
                return (await _context.SaveChangesAsync() >= 0);
            });
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }


        #endregion

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
