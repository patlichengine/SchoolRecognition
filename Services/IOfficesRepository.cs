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

        Task<IEnumerable<OfficesViewDto>> GetAllOfficesAsync();
        Task<CustomPagedList<OfficesViewDto>> GetAllOfficesAsPagedListAsync(OfficesResourceParams resourceParams);
        Task<OfficesViewDto> GetOfficesSingleOrDefaultAsync(Guid id);
        Task<OfficesViewDto> GetOfficesAllSchoolsAsync(Guid id);
        Task<OfficeViewPagedListSchoolsDto> GetOfficesSchoolsAsPagedListAsync(Guid id, SchoolsResourceParams resourceParams);
        Task<Guid?> CreateOfficeAsync(OfficesCreateDto _obj);
        Task<OfficesViewDto> UpdateOfficeAsync(OfficesCreateDto _obj);
        Task DeleteOfficeAsync(Guid id); //return type is void
        //
        Task<bool> CheckIfOfficeExists(string officeName);
        Task<bool> CheckIfOfficeExists(Guid id, string officeName);
        Task<OfficesCreationDependecyDto> GetOfficeCreationDepedencys();
    }
}
