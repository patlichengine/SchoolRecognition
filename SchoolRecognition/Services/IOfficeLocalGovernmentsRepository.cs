using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IOfficeLocalGovernmentsRepository
    {

        Task<IEnumerable<OfficeLocalGovernmentsViewDto>> List();
        Task<IEnumerable<OfficeLocalGovernmentsViewDto>> ListByStateId(Guid stateId);
        Task<PagedList<OfficeLocalGovernmentsViewDto>> PagedList(OfficeLocalGovernmentsResourceParams resourceParams);
        Task<OfficeLocalGovernmentsViewDto> Get(Guid id);
        Task<Guid?> Create(OfficeLocalGovernmentsCreateDto _obj);
        Task<IEnumerable<Guid?>> CreateMultiple(OfficeLocalGovernmentsCreateMultipleDto _obj);
        Task<OfficeLocalGovernmentsViewDto> Update(OfficeLocalGovernmentsCreateDto _obj);
        Task Delete(Guid id); //return type is void
        ///
        Task<bool> Exists(Guid localGovernmentdId, Guid officeId);
        Task<bool> Exists(Guid id, Guid localGovernmentdId, Guid officeId);

        Task<OfficeLocalGovernmentsCreationDependecyDto> GetCreationDependencys();
    }
}
