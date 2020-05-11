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
        Task<Guid?> CreateOfficeStateAsync(OfficeStatesCreateDto _obj);
        Task<OfficeStatesViewDto> UpdateOfficeStateAsync(OfficeStatesCreateDto _obj);
        Task DeleteOfficeStateAsync(Guid id); //return type is void
    }
}
