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

        Task<IEnumerable<PinsViewDto>> List();
        Task<IEnumerable<PinsViewDto>> ListActivePinsNOTInUseByRecognitionTypeId(Guid recognitionTypeId);
        Task<PagedList<PinsViewDto>> PagedList(PinsResourceParams resourceParams);
        Task<PinsViewDto> Get(Guid id);
        Task<PinsViewDto> GetIncludingListOfPinHistories(Guid id);
        Task<PinsViewDto> GetIncludingListOfSchoolPayments(Guid id);
        Task<PinsViewPagedListPinHistoriesDto> GetIncludingPagedListOfPinHistories(Guid id, PinHistoriesResourceParams resourceParams);
        
        Task<Guid?> Create(PinsCreateDto _obj);
        Task<bool> CreateMultiple(PinsCreateDto _obj);
        Task<PinsViewDto> Update(PinsUpdateDto _obj);
        Task Delete(Guid id); //return type is void
        //
        Task<int> CheckTotalActivePinsNOTInUse(Guid recognitionTypeId);
        Task<PinsStatisticsSummaryDto> Summary();
        Task<PinsCreationDependecyDto> GetCreationDepedencys();
    }
}
