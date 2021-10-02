using SchoolRecognition.ServiceLayer.Extensions;
using SchoolRecognition.ServiceLayer.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRecognition.ServiceLayer.BaseServices
{
    public interface IEFServices<Dto, DBTable> where Dto : class where DBTable : class
    {

        IQueryable<DBTable> GetQueryable();
        Task<IEnumerable<Dto>> ListAsNoTrackingAsync();
        Task<IEnumerable<Dto>> ListAsync();
        Task<PagedList<Dto>> PagedListAsNoTrackingAsync(BaseResourceParams<Dto> resourceParams, List<string> searchFields);
        Task<Dto> GetAsync(object primaryKey, string filterField);
        Task<Dto> GetAsNoTrackingAsync(object primaryKey, string filterField);
        Task<bool> CreateAsync(DBTable _obj);
        Task<bool> CreateAsync(Dto _obj);
        Task<bool> CreateMultipleAsync(List<Dto> _obj);
        Task<bool> CreateMultipleAsync(List<DBTable> _obj);
        Task<bool> UpdateAsync(Dto _obj);
        Task<bool> Remove(object primaryKey, string filterField);

        Task<bool> Remove(DBTable _obj);

        Task<bool> Remove(List<DBTable> _objs);

        //
        Task<bool> ExistsAsync(object primaryKey, string filterField);
        Task<bool> ExistsAsNoTrackingAsync(object primaryKey, string filterField);
        Task<bool> ExistsAsync(BaseResourceParams<Dto> resourceParameters, string filterField, bool shouldOnlyCheckEntitiesWithDifferentId);
    }
}
