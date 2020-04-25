﻿using AutoMapper;
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

    public class cRecognitionTypesRepository : IRecognitionTypesRepository
    {

        //private readonly ConnectionString _connectionString;
        //private IPinsRepository _pinService;
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;

        //public cRecognitionTypesRepository(ConnectionString connectionString)
        //{
        //    //_connectionString = connectionString;
        //    //_pinService = new cPinsRepository(_connectionString);

        //}

        public cRecognitionTypesRepository(SchoolRecognitionContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }



        public async Task<IEnumerable<RecognitionTypesDto>> Get()
        {
            try
            {
                //using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                //{
                //    var result = new List<RecognitionTypes>();

                //    //string strQuery = "Select * from dbo.RecognitionTypes;";
                //    //string strQuery = "SELECT * FROM dbo.RecognitionTypes AS recognitionType;";

                //    var _result = await _db.QueryAsync<RecognitionTypes>("dbo.procRecognitionTypesList", commandType: CommandType.StoredProcedure);

                //    result = _result.ToList();

                //    return result;
                //}
                var result = await _context.RecognitionTypes.ToListAsync();

                return _mapper.Map<IEnumerable<RecognitionTypesDto>>(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<CustomPagedList<RecognitionTypesDto>> Get(int? rangeIndex)
        {
            //Default Range Limit is 100 Rows
            int _lowerLimit = 0;
            int _upperLimit = 100;
            try
            {
                //Instantiate Array objects
                IList<RecognitionTypesDto> listOfDtos = new List<RecognitionTypesDto>();
                var cstPageList = new CustomPagedList<RecognitionTypesDto>();

                var count = await _context.RecognitionTypes.CountAsync();

                //Set Range of Row Based on rangeIndex parameter
                if (rangeIndex > 0)
                {
                    _lowerLimit = (rangeIndex.Value) * 100;
                    _upperLimit = (rangeIndex.Value + 1) * 100;
                }

                var _result = await _context.RecognitionTypes
                    .Skip(_lowerLimit)
                    .Take(_upperLimit)
                    .ToListAsync();
                //Assign count value
                cstPageList.TotalDBEntitysCount = count;
                //Map list of entities to list of dtos
                listOfDtos = _mapper.Map<IList<RecognitionTypesDto>>(_result);
                //Assign list value
                cstPageList.Entitys = listOfDtos.ToList();
                //Return value of upper and lower limit
                cstPageList.LowerLimit = _lowerLimit;
                cstPageList.UpperLimit = _upperLimit;

                return cstPageList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<CustomPagedList<RecognitionTypesDto>> Get(int? rangeIndex, string searchQuery)
        {
            //Default Range Limit is 100 Rows
            int _lowerLimit = 0;
            int _upperLimit = 100;
            //Default search query
            string _searchQuery = "";
            try
            {
                //Instantiate Array objects
                IList<RecognitionTypesDto> listOfDtos = new List<RecognitionTypesDto>();
                var cstPageList = new CustomPagedList<RecognitionTypesDto>();

                var count = await _context.RecognitionTypes.CountAsync();

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

                var _result = await _context.RecognitionTypes
                    .Where(
                    //Add all columns you wish to search
                    x => x.Name.ToUpper().Contains(searchQuery)
                    || x.Code.ToUpper().Contains(searchQuery)
                    )
                    .Skip(_lowerLimit)
                    .Take(_upperLimit)
                    .ToListAsync();
                //Assign count value
                cstPageList.TotalDBEntitysCount = count;
                //Map list of entities to list of dtos
                listOfDtos = _mapper.Map<IList<RecognitionTypesDto>>(_result);
                //Assign list value
                cstPageList.Entitys = listOfDtos.ToList();
                //Return value of upper and lower limit
                cstPageList.LowerLimit = _lowerLimit;
                cstPageList.UpperLimit = _upperLimit;

                return cstPageList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<CustomPagedList<RecognitionTypesDto>> Get(int? rangeIndex, string searchQuery, string orderCriteria, bool reverseOrder)
        {
            //Default Range Limit is 100 Rows
            int _lowerLimit = 0;
            int _upperLimit = 100;
            //Default search query
            string _searchQuery = "";
            try
            {
                //Instantiate Array objects
                IList<RecognitionTypesDto> listOfDtos = new List<RecognitionTypesDto>();
                IList<RecognitionTypes> listResult = new List<RecognitionTypes>();
                var cstPageList = new CustomPagedList<RecognitionTypesDto>();

                var count = await _context.RecognitionTypes.CountAsync();

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
                var orderParameter = typeof(RecognitionTypes).GetProperty(orderCriteria);

                ///Order or reverse order  by a property
                if (reverseOrder == false)
                {
                    var _result = await _context.RecognitionTypes
                    .Where(
                    //Add all columns you wish to search
                    x => x.Name.ToUpper().Contains(searchQuery)
                    || x.Code.ToUpper().Contains(searchQuery)
                    )
                    .OrderBy(x => orderParameter.GetValue(x, null))
                    .Skip(_lowerLimit)
                    .Take(_upperLimit)
                    .ToListAsync();

                    listResult = _result;
                }
                else
                {
                    var _result = await _context.RecognitionTypes
                    .Where(
                    //Add all columns you wish to search
                    x => x.Name.ToUpper().Contains(searchQuery)
                    || x.Code.ToUpper().Contains(searchQuery)
                    )
                    .OrderByDescending(x => orderParameter.GetValue(x, null))
                    .Skip(_lowerLimit)
                    .Take(_upperLimit)
                    .ToListAsync();
                    //
                    listResult = _result;
                }
                //Assign count value
                cstPageList.TotalDBEntitysCount = count;
                //Map list of entities to list of dtos
                listOfDtos = _mapper.Map<IList<RecognitionTypesDto>>(listResult);
                //Assign list value
                cstPageList.Entitys = listOfDtos.ToList();
                //Return value of upper and lower limit
                cstPageList.LowerLimit = _lowerLimit;
                cstPageList.UpperLimit = _upperLimit;

                return cstPageList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<RecognitionTypesDto> Get(Guid id)
        {
            try
            {

                //using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                //{
                //    RecognitionTypes result = null;
                //    List<Pins> _pins = new List<Pins>();

                //    if (id != Guid.Empty)
                //    {
                //        //string strQuery = "Select * from dbo.RecognitionTypes WHERE ID = @_id;";

                //        //var _result = await _db.QueryFirstOrDefaultAsync<RecognitionTypes>(strQuery, new { _id = id });
                //        var _result = await _db.QueryFirstOrDefaultAsync<RecognitionTypes>("dbo.procRecognitionTypesDetailById", new { ID = id }, commandType: CommandType.StoredProcedure);

                //        if (_result != null)
                //        {
                //            var pins = await _pinService.GetPinsByRecognitionTypeId(id);

                //            result = _result;
                //            result.Pins = pins;

                //        }

                //        return result;

                //    }

                //    return result;
                //}

                RecognitionTypesDto result = null;
                List<Pins> _pins = new List<Pins>();

                if (id != Guid.Empty)
                {
                    var _result = await _context.RecognitionTypes.Include(y => y.Pins).Where(x => x.Id == id).FirstOrDefaultAsync();

                    if (_result != null)
                    {
                        result = _mapper.Map<RecognitionTypesDto>(_result);
                        result.RecognitionTypePins = _mapper.Map<IEnumerable<PinsViewDto>>(_result.Pins);
                    }
                }

                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Guid?> Create(RecognitionTypesDto _obj)
        {

            try
            {
                //using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                //{
                //    //string strQuery = "INSERT INTO dbo.RecognitionTypes (Id,Name,Code)" +
                //    //" VALUES (@Id,@Name,@Code);";

                //    Guid? returnId = Guid.Empty;
                //    if (_obj != null)
                //    {
                //        _obj.Id = Guid.NewGuid();

                //        var _result = await _db.QueryFirstAsync<Guid>("dbo.procRecognitionTypesCreate", new
                //        {
                //            Id = _obj.Id,
                //            Name = _obj.Name,
                //            Code = _obj.Code
                //        }, commandType: CommandType.StoredProcedure);

                //        returnId = _result;
                //    }

                //    return returnId;
                //}
                Guid? returnId = null;
                if (_obj != null)
                {
                    RecognitionTypes entity = _mapper.Map<RecognitionTypes>(_obj);
                    //
                    entity.Id = Guid.NewGuid();
                    await _context.RecognitionTypes.AddAsync(entity);
                    await this.Save();
                }
                return returnId;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<RecognitionTypesDto> Update(RecognitionTypesDto _obj)
        {

            try
            {
                //using (IDbConnection _db = new SqlConnection(_connectionString.Value))
                //{
                //    //string strQuery = "UPDATE dbo.RecognitionTypes SET " +
                //    //"Name = @Name, " +
                //    //"Code = @Code, " +
                //    //"WHERE Id = @Id;";

                //    Guid? returnId = Guid.Empty;
                //    if (_obj != null)
                //    {

                //        var _result = await _db.ExecuteAsync("dbo.procRecognitionTypesUpdate", new
                //        {
                //            Id = _obj.Id,
                //            Name = _obj.Name,
                //            Code = _obj.Code
                //            //CreatedBy = _obj.CreatedBy,
                //            //DateCreated = _obj.DateCreated
                //        }, commandType: CommandType.StoredProcedure);

                //    }

                //    return _obj;
                //}
                if (_obj != null && _obj.Id != Guid.Empty)
                {
                    var entity = _mapper.Map<RecognitionTypes>(_obj);

                    _context.Update(entity);
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
                //    //string strQuery = "DELETE FROM dbo.RecognitionTypes WHERE Id = @Id";

                //    if (id != Guid.Empty)
                //    {
                //        var _result = await _db.ExecuteAsync("dbo.procRecognitionTypesDelete", new { Id = id }, commandType: CommandType.Text);
                //    }
                //}
                if (id != Guid.Empty)
                {
                    var entity = await _context.RecognitionTypes.Where(x => x.Id == id).FirstOrDefaultAsync();
                    if (entity != null)
                    {
                        _context.Remove(entity);
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


    }
}
