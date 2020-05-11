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
        Task<OfficesViewDto> GetOfficesAllOfficeStatesAsync(Guid id);
        Task<OfficeViewPagedListOfficeStatesDto> GetOfficesOfficeStatesAsPagedListAsync(Guid id, OfficeStatesResourceParams resourceParams);
        Task<Guid?> CreateOfficesAsync(OfficesCreateDto _obj);
        Task<OfficesViewDto> UpdateOfficeAsync(OfficesCreateDto _obj);
        Task DeleteOfficeAsync(Guid id); //return type is void
    }
}
