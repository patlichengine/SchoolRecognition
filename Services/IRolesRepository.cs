using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IRolesRepository
    {
        public Task<RolesDto> GetRole(Guid id);

        public Task<IEnumerable<RolesDto>> GetRoles();
    }
}
