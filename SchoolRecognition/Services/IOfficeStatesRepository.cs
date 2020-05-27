using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IOfficeStatesRepository
    {

        Task<IEnumerable<OfficeStatesViewDto>> List();
        Task<PagedList<OfficeStatesViewDto>> PagedList(OfficeStatesResourceParams resourceParams);
        Task<OfficeStatesViewDto> Get(Guid id);
        Task<Guid?> Create(OfficeStatesCreateDto _obj);
        Task<IEnumerable<Guid?>> CreateMultiple(OfficeStatesCreateMultipleDto _obj);
        Task<OfficeStatesViewDto> Update(OfficeStatesCreateDto _obj);
        Task Delete(Guid id); //return type is void
        ///
        Task<bool> Exists(Guid statedId, Guid officeId);
        Task<bool> Exists(Guid id, Guid statedId, Guid officeId);
        Task<OfficeStatesCreationDependecyDto> GetCreationDependencys();
    }
}
