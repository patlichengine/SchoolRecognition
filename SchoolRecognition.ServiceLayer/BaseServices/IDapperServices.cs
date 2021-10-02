using Dapper;
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
    public interface IDapperServices<Dto, DBTable> where Dto : class where DBTable : class
    {
        Task<IEnumerable<Dto>> ListAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<PagedList<Dto>> PagedListAsync(BaseResourceParams<Dto> resourceParams, string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<Dto> GetAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<bool> CreateAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
  
        Task<bool> UpdateAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<bool> RemoveAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        //
        Task<bool> ExistsAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        
    }
}
