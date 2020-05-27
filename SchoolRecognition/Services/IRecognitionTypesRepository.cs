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

        Task<IEnumerable<RecognitionTypesViewDto>> List();
        Task<PagedList<RecognitionTypesViewDto>> PagedList(RecognitionTypesResourceParams resourceParams);
        Task<RecognitionTypesViewDto> Get(Guid id);
        Task<RecognitionTypesViewDto> GetIncludingListOfPins(Guid id);
        Task<RecognitionTypesViewPagedListPinsDto> GetIncludingPagedListOfPins(Guid id, PinsResourceParams resourceParams);
        Task<Guid?> Create(RecognitionTypesCreateDto _obj);
        Task<RecognitionTypesViewDto> Update(RecognitionTypesCreateDto _obj);
        Task Delete(Guid id); //return type is void
        ///

        Task<bool> Exists(string name, string code);
        Task<bool> Exists(Guid id, string name, string code);

    }
}
