using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.Classes;
using SchoolRecognition.DbContexts;
using SchoolRecognition.Entities;
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
