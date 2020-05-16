using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IStatesRepository
    {


        Task<IEnumerable<StatesViewDto>> GetAllStatesAsync();
        Task<CustomPagedList<StatesViewDto>> GetAllStatesAsPagedListAsync(StatesResourceParams resourceParams);
        Task<StatesViewDto> GetStatesSingleOrDefaultAsync(Guid id);
        Task<Guid?> CreateStateAsync(StatesCreateDto _obj);
        Task<StatesViewDto> UpdateStateAsync(StatesCreateDto _obj);
        Task DeleteStateAsync(Guid id); //return type is void
        ///

        Task<bool> CheckIfStateExists(string stateName);
        Task<bool> CheckIfStateExists(Guid id, string stateName);

    }
}
