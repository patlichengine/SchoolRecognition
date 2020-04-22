using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IOfficeRepository
    {
        public Task<OfficesDto> GetOffice(Guid id);

        public Task<IEnumerable<OfficesDto>> GetOffices();

        public Task<IEnumerable<RolesDto>> GetOfficeSchools(Guid officeId);

        public Task<IEnumerable<RolesDto>> GetOfficeSchools(Guid officeId, Guid schoolId);

        public Task<IEnumerable<RolesDto>> GetOfficeCentres(Guid officeId);
        public Task<IEnumerable<RolesDto>> GetOfficeCentres(Guid officeId, Guid centreId);
    }
}
