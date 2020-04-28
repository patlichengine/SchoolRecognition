using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface ICentresRepository
    {
        Task<CentresDto> GetCentreByCentreNumber(string centreNumber);

        Task<IEnumerable<CentresDto>> GetAllCentres();        

        //Task<CentresDto> UpdateCentre(Guid id, CentresDto centre);       

        //Task<bool> CentreExists(Guid Id);
        //Task<bool> Save();
    }
}
