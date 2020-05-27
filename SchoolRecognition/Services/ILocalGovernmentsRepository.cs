using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface ILocalGovernmentsRepository
    {
        Task<IEnumerable<LocalGovernmentsViewDto>> List();
        Task<IEnumerable<LocalGovernmentsViewDto>> ListByStateId(Guid stateId);
    }
}
