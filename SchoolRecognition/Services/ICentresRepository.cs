using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface ICentresRepository
    {

        Task<IEnumerable<CentresViewDto>> List();
        Task<PagedList<CentresViewDto>> PagedList(CentresResourceParams resourceParams);
        Task<CentresViewDto> Get(Guid id);
        Task<CentresViewDto> GetByCentreNumber(string centerNo);
        Task<Guid?> Create(CentresCreateDto _obj);
        Task<CentresViewDto> Update(CentresCreateDto _obj);
        Task Delete(Guid id); //return type is void
        ///
        Task<bool> Exists(string centreName);
        Task<bool> Exists(Guid id, string centreName);
        Task<bool> Exists(string centreName, string centreNo);
        Task<bool> Exists(Guid id, string centreName, string centreNo);
    }
}
