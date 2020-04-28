using SchoolRecognition.Entities;
using SchoolRecognition.Extensions;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IRecognitionTypesRepository
    {

        Task<IEnumerable<RecognitionTypesDto>> GetAll();
        Task<CustomPagedList<RecognitionTypesDto>> Get(int? rangeIndex);
        Task<CustomPagedList<RecognitionTypesDto>> Get(int? rangeIndex, string searchQuery);
        Task<CustomPagedList<RecognitionTypesDto>> GetAndOrderByName(int? rangeIndex, string searchQuery, bool reverseOrder);
        Task<RecognitionTypesDto> Get(Guid id);
        Task<RecognitionTypesViewPinsDto> GetDetailsAndIncludePins(Guid id, int? rangeIndex);
        Task<RecognitionTypesViewPinsDto> GetDetailsAndIncludePins(Guid id, int? rangeIndex, string searchQuery);
        Task<RecognitionTypesViewPinsDto> GetDetailsAndIncludePinsAndOrderByDateCreated(Guid id, int? rangeIndex, string searchQuery, bool reverseOrder);
        Task<RecognitionTypesViewPinsDto> GetDetailsAndIncludePinsAndOrderBySerialPin(Guid id, int? rangeIndex, string searchQuery, bool reverseOrder);
        Task<Guid?> Create(RecognitionTypesDto _obj);
        Task<RecognitionTypesDto> Update(RecognitionTypesDto _obj);
        Task Delete(Guid id); //return type is void
    }
}
