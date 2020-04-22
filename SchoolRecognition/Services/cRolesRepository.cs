using AutoMapper;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public class cRolesRepository : IRolesRepository, IDisposable
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;

        public cRolesRepository(SchoolRecognitionContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public  Task<RolesDto> GetRole(Guid id)
        {


            throw new NotImplementedException();


        }

        public  Task<IEnumerable<RolesDto>> GetRoles()
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
