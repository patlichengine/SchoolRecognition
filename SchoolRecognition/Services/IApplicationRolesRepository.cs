using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IApplicationRolesRepository
    {
        public Task<ApplicationRolesDto> GetApplicationRole(Guid id);

        public Task<IEnumerable<ApplicationRolesDto>> GetApplicationRoles();
    }
}
