using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IPinsRepository
    {
         Task<IEnumerable<PinsDto>> GetPins();
         Task<IEnumerable<PinsDto>> GetPinsByRecognitionType(Guid recognitionTypeID);
    }
}
