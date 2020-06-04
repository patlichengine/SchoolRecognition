using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IOfficesRepository
    {

        Task<IEnumerable<OfficesViewDto>> List();
        Task<IEnumerable<OfficesViewDto>> ListByOfficeTypeId(Guid officeTypeId);
        Task<PagedList<OfficesViewDto>> PagedList(OfficesResourceParams resourceParams);
        Task<OfficesViewDto> Get(Guid id);
        Task<Guid> GetCurrentUserOfficeId();
        Task<OfficesViewPagedListSchoolsDto> GetIncludingPagedListOfSchools(Guid id, SchoolsResourceParams resourceParams);
        Task<OfficeViewPagedListOfficeLocalGovernmentsDto> GetIncludingPagedListOfOfficeLocalGovernments(Guid id, OfficeLocalGovernmentsResourceParams resourceParams);
        Task<Guid?> Create(OfficesCreateDto _obj);
        Task<OfficesViewDto> Update(OfficesCreateDto _obj);
        Task Delete(Guid id); //return type is void
        //
        Task<bool> Exists(string officeName);
        Task<bool> Exists(Guid id, string officeName);
        Task<OfficesCreationDependecyDto> GetCreationDependencys();
    }
}
