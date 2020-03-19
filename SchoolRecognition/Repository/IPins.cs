using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Repository
{
    public interface IPins
    {
        Task<List<Pins>> Get();
        Task<List<Pins>> GetPinsByRecognitionTypeId(Guid recognitionTypeId);
        Task<Pins> Get(Guid id);
        Task<Guid?> Create(Pins _obj);
        Task<bool> CreateSeveralPins(Pins _obj, int numberOfPinsToGenerate);
        Task<Pins> Update(Pins _obj);
        Task Delete(Guid id); //return type is void
    }
}
