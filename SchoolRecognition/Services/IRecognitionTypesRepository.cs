using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IRecognitionTypesRepository
    {

        Task<IEnumerable<RecognitionTypesDto>> Get();
        Task<RecognitionTypesDto> Get(Guid id);
        Task<Guid?> Create(RecognitionTypesDto _obj);
        Task<RecognitionTypesDto> Update(RecognitionTypesDto _obj);
        Task Delete(Guid id); //return type is void
    }
}
