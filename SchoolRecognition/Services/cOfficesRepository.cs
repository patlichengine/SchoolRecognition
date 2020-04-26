using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public class cOfficesRepository : IOfficeRepository, IDisposable
    {
        public Task<OfficesDto> GetOffice(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RolesDto>> GetOfficeCentres(Guid officeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OfficesDto>> GetOffices()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RolesDto>> GetOfficeSchools(Guid officeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RolesDto>> GetOfficeSchools(Guid officeId, Guid schoolId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RolesDto>> GetOfficeCentres(Guid officeId, Guid centreId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }

        
    }
}
