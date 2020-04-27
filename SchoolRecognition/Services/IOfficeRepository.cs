using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IOfficeRepository
    {
         Task<OfficesDto> GetOffice(Guid id);

         Task<IEnumerable<OfficesDto>> GetOffices();

         Task<IEnumerable<RolesDto>> GetOfficeSchools(Guid officeId);

         Task<IEnumerable<RolesDto>> GetOfficeSchools(Guid officeId, Guid schoolId);

         Task<IEnumerable<RolesDto>> GetOfficeCentres(Guid officeId);
         Task<IEnumerable<RolesDto>> GetOfficeCentres(Guid officeId, Guid centreId);
    }
}
