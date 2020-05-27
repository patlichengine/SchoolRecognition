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
    public interface IRecognitionTypesRepository
    {

        Task<IEnumerable<RecognitionTypesViewDto>> GetAllRecognitionTypesAsync();
        Task<CustomPagedList<RecognitionTypesViewDto>> GetAllRecognitionTypesAsPagedListAsync(RecognitionTypesResourceParams resourceParams);
        Task<RecognitionTypesViewDto> GetRecognitionTypesSingleOrDefaultAsync(Guid id);
        Task<RecognitionTypesViewDto> GetRecognitionTypesAllPinsAsync(Guid id);
        Task<RecognitionTypesViewPagedListPinsDto> GetRecognitionTypesPinsAsPagedListAsync(Guid id, PinsResourceParams resourceParams);
        Task<Guid?> CreateRecognitionTypeAsync(RecognitionTypesCreateDto _obj);
        Task<RecognitionTypesViewDto> UpdateRecognitionTypeAsync(RecognitionTypesCreateDto _obj);
        Task DeleteRecognitionTypeAsync(Guid id); //return type is void
        ///

        Task<bool> CheckIfRecognitionTypeExists(string name, string code);
        Task<bool> CheckIfRecognitionTypeExists(Guid id, string name, string code);

    }
}
