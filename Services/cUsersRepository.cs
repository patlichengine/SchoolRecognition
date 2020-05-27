using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.DbContexts;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public class cUsersRepository : IUserRepository, IDisposable
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        public cUsersRepository(SchoolRecognitionContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }





        public async Task<UserDto> Authenticate(string username, byte[] password)
        {
            return await Task.Run(async () =>
            {

                var result = await _context.Users.FirstOrDefaultAsync(c => c.EmailAddress == username && c.Password==password);
                return _mapper.Map<UserDto>(result);
            });
        }
        public async Task<UserDto> GetById(Guid id)
        {
            return await Task.Run(async () =>
            {

                var result = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
                return _mapper.Map<UserDto>(result);
            });
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
