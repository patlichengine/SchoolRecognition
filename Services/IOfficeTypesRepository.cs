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

        Task<IEnumerable<OfficeTypesViewDto>> GetAllOfficeTypesAsync();
        Task<CustomPagedList<OfficeTypesViewDto>> GetAllOfficeTypesAsPagedListAsync(OfficeTypesResourceParams resourceParams);
        Task<OfficeTypesViewDto> GetOfficeTypesSingleOrDefaultAsync(Guid id);
        Task<OfficeTypesViewDto> GetOfficeTypesAllOfficesAsync(Guid id);
        Task<OfficeTypeViewPagedListOfficesDto> GetOfficeTypesOfficesAsPagedListAsync(Guid id, OfficesResourceParams resourceParams);
        Task<Guid?> CreateOfficeTypeAsync(OfficeTypesCreateDto _obj);
        Task<OfficeTypesViewDto> UpdateOfficeTypeAsync(OfficeTypesCreateDto _obj);
        Task DeleteOfficeTypeAsync(Guid id); //return type is void
        ///

        Task<bool> CheckIfOfficeTypeExists(string officeTypeDescription);
        Task<bool> CheckIfOfficeTypeExists(Guid id, string officeTypeDescription);

    }
}
