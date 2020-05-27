using SchoolRecognition.Entities;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IPinsRepository
    {

        Task<IEnumerable<PinsViewDto>> GetAllPinsAsync();
        Task<CustomPagedList<PinsViewDto>> GetAllPinsAsPagedListAsync(PinsResourceParams resourceParams);
        Task<PinsViewDto> GetPinsSingleOrDefaultAsync(Guid id);
        Task<PinsViewDto> GetPinsAllPinHistoriesAsync(Guid id);
        Task<PinsViewDto> GetPinsAllSchoolPaymentsAsync(Guid id);
        Task<PinsViewPagedListPinHistoriesDto> GetPinsPinHistoriesAsPagedListAsync(Guid id, PinHistoriesResourceParams resourceParams);
        
        Task<Guid?> CreatePinsAsync(PinsCreateDto _obj);
        Task<bool> CreateMultiplePinsAsync(PinsCreateDto _obj);
        Task<PinsViewDto> UpdatePinAsync(PinsUpdateDto _obj);
        Task DeletePinAsync(Guid id); //return type is void
        //
        Task<int> CheckNumberOfActivePinsNOTInUseAsync();
        Task<PinsStatisticsSummaryDto> GetPinsStatisticSummaryAsync();
        Task<PinsCreationDependecyDto> GetPinsCreationDepedencys();
    }
}
