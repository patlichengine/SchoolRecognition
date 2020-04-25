using SchoolRecognition.Entities;
using SchoolRecognition.Extensions;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IPinsRepository
    {
        Task<IEnumerable<PinsViewDto>> Get();
        Task<CustomPagedList<PinsViewDto>> Get(int? rangeIndex);
        Task<CustomPagedList<PinsViewDto>> Get(int? rangeIndex, string searchQuery);
        Task<CustomPagedList<PinsViewDto>> Get(int? rangeIndex, string searchQuery, string orderCriteria, bool reverseOrder);
        Task<IEnumerable<PinsViewDto>> GetPinsByRecognitionTypeId(Guid recognitionTypeId);
        Task<PinsViewDto> Get(Guid id);
        Task<Guid?> Create(PinsCreateDto _obj);
        Task<bool> CreateSeveralPins(PinsCreateDto _obj);
        Task<PinsUpdateDto> Update(PinsUpdateDto _obj);
        Task Delete(Guid id); //return type is void
    }
}
