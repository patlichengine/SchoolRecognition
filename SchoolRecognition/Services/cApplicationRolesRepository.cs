using AutoMapper;
using SchoolRecognition.DbContexts;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public class cApplicationRolesRepository : IApplicationRolesRepository, IDisposable
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;

        public cApplicationRolesRepository(SchoolRecognitionContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ApplicationRolesDto> GetApplicationRole(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var result = _context.ApplicationRoles.FirstOrDefault(c => c.Id == id);
            return _mapper.Map<ApplicationRolesDto>(result);
        }

        public async Task<IEnumerable<ApplicationRolesDto>> GetApplicationRoles()
        {
            var result = _context.ApplicationRoles.ToList<ApplicationRoles>();
            return _mapper.Map<IEnumerable<ApplicationRolesDto>>(result);
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
