using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IOfficeTypesRepository
    {

        Task<IEnumerable<OfficeTypesViewDto>> List();
        Task<PagedList<OfficeTypesViewDto>> PagedList(OfficeTypesResourceParams resourceParams);
        Task<OfficeTypesViewDto> Get(Guid id);
        Task<OfficeTypesViewDto> GetIncludingListOfOffices(Guid id);
        Task<OfficeTypeViewPagedListOfficesDto> GetIncludingPagedListOfOffices(Guid id, OfficesResourceParams resourceParams);
        Task<Guid?> Create(OfficeTypesCreateDto _obj);
        Task<OfficeTypesViewDto> Update(OfficeTypesCreateDto _obj);
        Task Delete(Guid id); //return type is void
        ///

        Task<bool> Exists(string officeTypeDescription);
        Task<bool> Exists(Guid id, string officeTypeDescription);

    }
}
