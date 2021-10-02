using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.DBInfrastructure.DbContexts;
using SchoolRecognition.DBInfrastructure.Helpers;
using SchoolRecognition.ServiceLayer.Extensions;
using SchoolRecognition.ServiceLayer.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRecognition.ServiceLayer.BaseServices
{
    public class DapperServices<Dto, DBTable> : IDapperServices<Dto, DBTable> where Dto : class where DBTable : class
    {
        private readonly ConnectionString _connectionString;
        private readonly IMapper _mapper;

        protected WSchoolRecognitionContext DbContext
        {
            get;
            set;
        }
        protected DbSet<DBTable> DbSet
        {
            get;
            set;
        }

        public DapperServices(WSchoolRecognitionContext context, ConnectionString connectionString, IMapper mapper)
        {
            this.DbContext = context ?? throw new ArgumentNullException(nameof(context));

            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            this.DbSet = this.DbContext.Set<DBTable>();
        }

        public async Task<bool> CreateAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection _connection = new SqlConnection(_connectionString.Value))
            {
                _connection.Open();

                var affectedRows = await _connection.ExecuteAsync(sp, parms, commandType: commandType);

            }

            return true;

        }

        public async Task<bool> ExistsAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            bool returnValue;
            using (IDbConnection _connection = new SqlConnection(_connectionString.Value))
            {
                _connection.Open();

                returnValue = await _connection.QueryFirstOrDefaultAsync<bool>(sp, parms, commandType: commandType);

            }

            return returnValue;

        } 

        public async Task<Dto> GetAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            Dto returnValue = null;
            using (IDbConnection _connection = new SqlConnection(_connectionString.Value))
            {
                _connection.Open();


                var dbResult = await _connection.QueryFirstOrDefaultAsync<DBTable>(sp, parms, commandType: commandType);

                returnValue = _mapper.Map<Dto>(dbResult);

            }

            return returnValue;
        } 


        public async Task<IEnumerable<Dto>> ListAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            IEnumerable<Dto> returnValue = new List<Dto>();
            using (IDbConnection _connection = new SqlConnection(_connectionString.Value))
            {
                IEnumerable<DBTable> dbResult = new List<DBTable>();
                _connection.Open();


                dbResult = await _connection.QueryAsync<DBTable>(sp, parms, commandType: commandType);

                returnValue = _mapper.Map<IEnumerable<Dto>>(dbResult.ToList()).ToList();

            }

            return returnValue;
        }


        public async Task<PagedList<Dto>> PagedListAsync(BaseResourceParams<Dto> resourceParams, string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            //instantiate return value
            PagedList<Dto> returnValue = PagedList<Dto>
                        .Create(Enumerable.Empty<Dto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);
            //
            using (IDbConnection _connection = new SqlConnection(_connectionString.Value))
            {
                IEnumerable<DBTable> dbResult = new List<DBTable>();
                _connection.Open();

                var entityCount = await this.DbSet.CountAsync();


                parms.Add("PageNumber", resourceParams.PageNumber);
                parms.Add("PageSize", resourceParams.PageSize);
                parms.Add("OrderBy", resourceParams.OrderBy);
                parms.Add("FilterParam", resourceParams.SearchQuery);

                dbResult = await _connection.QueryAsync<DBTable>(sp, parms, commandType: commandType);

                List<Dto> mappedResult = _mapper.Map<IEnumerable<Dto>>(dbResult.ToList()).ToList();
                returnValue = new PagedList<Dto>(mappedResult, entityCount, resourceParams.PageNumber, resourceParams.PageSize);


            }

            return returnValue;
        }

        public async Task<bool> RemoveAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection _connection = new SqlConnection(_connectionString.Value))
            {
                _connection.Open();

                var affectedRows = await _connection.ExecuteAsync(sp, parms, commandType: commandType);

            }

            return true;

        }

        public async Task<bool> UpdateAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection _connection = new SqlConnection(_connectionString.Value))
            {
                _connection.Open();

                var affectedRows = await _connection.ExecuteAsync(sp, parms, commandType: commandType);

            }

            return true;

        }

    }
}
