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

        Task<IEnumerable<OfficeStatesViewDto>> GetAllOfficeStatesAsync();
        Task<CustomPagedList<OfficeStatesViewDto>> GetAllOfficeStatesAsPagedListAsync(OfficeStatesResourceParams resourceParams);
        Task<OfficeStatesViewDto> GetOfficeStatesSingleOrDefaultAsync(Guid id);
        Task<OfficeStatesViewDto> GetOfficeStatesByOfficeIdSingleOrDefaultAsync(Guid officeId);
        Task<Guid?> CreateOfficeStateAsync(OfficeStatesCreateDto _obj);
        Task<OfficeStatesViewDto> UpdateOfficeStateAsync(OfficeStatesCreateDto _obj);
        Task DeleteOfficeStateAsync(Guid id); //return type is void
        ///
        Task<bool> CheckIfOfficeStateExists(Guid statedId, Guid officeId);
        Task<bool> CheckIfOfficeStateExists(Guid id, Guid statedId, Guid officeId);
    }
}
