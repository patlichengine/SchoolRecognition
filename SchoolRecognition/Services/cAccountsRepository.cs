using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.Classes;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public class cAccountsRepository : IAccountsRepository, IDisposable
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;

        public cAccountsRepository(SchoolRecognitionContext  context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> AccountExists()
        {
            return await Task.Run(async () =>
            {
                bool result = await _context.Users.AnyAsync();
                return result;
            });
           
        }

        public async Task<bool> AccountExists(Guid userId)
        {
            return await Task.Run(async () => {
                if (userId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(userId));
                }

                return await _context.Users.AnyAsync(a => a.Id == userId);
            });
            
        }


        //create account/use method
        public async void CreateAccount(RegisterViewModel _obj)
        {
            await Task.Run(async () => {
                if (_obj == null)
                {
                    throw new ArgumentNullException(nameof(_obj));
                }
                var mPassword = Encryption.EncryptPassword(_obj.Password);
                //Add the user details
                await _context.Users.AddAsync(new Users
                {
                    Id = Guid.NewGuid(),
                    Surname = _obj.Surname,
                    Othernames = _obj.OtherName,
                    EmailAddress = _obj.Email,
                    Password = mPassword,
                    PhoneNo = _obj.PhoneNo
                });
            });
            
        }



        
        //use user account by id
        public async Task<AccountsDto> GetAccount(Guid id)
        {
            return await Task.Run(async () =>
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var result = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
                //return the mapped object
                return _mapper.Map<AccountsDto>(result);
            });
            
        }

        public async Task<AccountsDto> GetAccount(string emailAddress, string password)
        {
            return await Task.Run(async () =>
            {
                if (string.IsNullOrEmpty(emailAddress))
                {
                    throw new ArgumentNullException(nameof(emailAddress));
                }

                if (string.IsNullOrEmpty(password))
                {
                    throw new ArgumentNullException(nameof(password));
                }

                //get password in byte
                var pwd = Encryption.EncryptPassword(password);

                var result = await _context.Users.FirstOrDefaultAsync(c => c.EmailAddress == emailAddress && c.Password == pwd);
                //return the mapped object
                return _mapper.Map<AccountsDto>(result);
            });
        }

        public async Task<IEnumerable<AccountsDto>> GetAccounts()
        {
            return await Task.Run(async () =>
            {
                var result = await _context.Users.ToListAsync<Users>();
                return _mapper.Map<IEnumerable<AccountsDto>>(result);

            });
        }

        public async Task<IEnumerable<AccountsDto>> GetRoleAccounts(Guid roleId)
        {
            return await Task.Run(async () =>
            {
                var result = await _context.Users.Where(c => c.RoleId == roleId).ToListAsync<Users>();
                return _mapper.Map<IEnumerable<AccountsDto>>(result);
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
