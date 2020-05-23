using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.Classes;
using SchoolRecognition.DbContexts;
using SchoolRecognition.Entities;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public class cAccountsRepository : IDisposable, IAccountsRepository
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public cAccountsRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        }

        public async Task<bool> AccountExists()
        {
            return await Task.Run(async () =>
            {
                bool result = await _context.ApplicationUsers.AnyAsync();
                return result;
            });

        }

        public async Task<bool> AccountExists(Guid userId)
        {
            return await Task.Run(async () =>
            {
                if (userId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(userId));
                }

                return await _context.ApplicationUsers.AnyAsync(a => a.Id == userId);
            });

        }

        private async Task<bool> AccountExistsByEmail(string emailAddress)
        {
            return await Task.Run(async () =>
            {
                if (string.IsNullOrWhiteSpace(emailAddress))
                {
                    throw new ArgumentNullException(nameof(emailAddress));
                }

                return await _context.ApplicationUsers.AnyAsync(a => a.EmailAddress == emailAddress);
            });

        }



        //create account/use method

        public async Task<AccountsDto> CreateAccount(AccountsCreateDto user)
        {
            return await Task.Run(async () =>
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }
                //check if account already exists
                var exists = await AccountExistsByEmail(user.EmailAddress);

                var userEntity = _mapper.Map<ApplicationUsers>(user);

                if (!exists)
                {

                    //Get the default role value
                    var role = _context.ApplicationRoles.Where(c => c.IsDefault).FirstOrDefault();

                    userEntity.Id = Guid.NewGuid();
                    userEntity.RoleId = role.Id;

                    //byte[] passWd = Encryption.EncryptPassword(user.Password);
                    //userEntity.Password = passWd;

                    //check if user exists
                    _context.ApplicationUsers.Add(userEntity);

                    //call the save method
                    bool saveResult = await Save();
                }

                return _mapper.Map<AccountsDto>(userEntity);
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

                var result = await _context.ApplicationUsers.Include(x => x.Rank)
                    .Include(x => x.Role).Include(x => x.ApplicationUserStatesCreatedByNavigation)
                    .Include(x => x.ApplicationUserStatesUser).Include(x => x.AuditTrail)
                    .Include(x => x.CentreSanctions).Include(x => x.Centres)
                    .Include(x => x.FacilitySettings).Include(x => x.Offices)
                    .Include(x => x.PinHistories).Include(x => x.Pins)
                    .Include(x => x.SchoolFacilities).Include(x => x.SchoolPayments)
                    .Include(x => x.SchoolStaffDegrees).FirstOrDefaultAsync(c => c.Id == id);
                //return the mapped object
                return _mapper.Map<AccountsDto>(result);
            });

        }

        public async Task<bool> GetAccountByMail(string email)
        {
            return await Task.Run(async () =>
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentNullException(nameof(email));
                }

                var result = await _context.ApplicationUsers.FirstOrDefaultAsync(c => c.EmailAddress == email.Trim());
                if (result == null) return false;
                //return the mapped object
                return true;
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

                var result = await _context.ApplicationUsers.Include(c=>c.Role).FirstOrDefaultAsync(c => c.EmailAddress == emailAddress && c.Password == pwd);

                var mappedData = _mapper.Map<AccountsDto>(result);
                //mappedData.Role = result.Role;
                //return the mapped object
                return mappedData;
            });
        }


        public async Task<IEnumerable<AccountsDto>> GetAccounts()
        {
            return await Task.Run(async () =>
            {
                var result = await _context.ApplicationUsers.ToListAsync<ApplicationUsers>();
                return _mapper.Map<IEnumerable<AccountsDto>>(result);

            });
        }

        public async Task<PagedList<AccountsDto>> GetAccounts(UserResourceParameters usersResourceParameters)
        {
            return await Task.Run(async () =>
            {
                if (usersResourceParameters == null)
                {
                    throw new ArgumentNullException(nameof(usersResourceParameters));
                }

                //cast the collection into an IQueriable object 
                //ApplicationUserStatesCreatedByNavigation ApplicationUserStatesUser AuditTrail CentreSanctions Centres FacilitySettings 
                //Offices PinHistories Pins SchoolFacilities SchoolPayments SchoolStaffDegrees
                var collection = _context.ApplicationUsers.Include(x=>x.Rank)
                    .Include(x=>x.Role).Include(x=>x.ApplicationUserStatesCreatedByNavigation)
                    .Include(x=>x.ApplicationUserStatesUser).Include(x=>x.AuditTrail)
                    .Include(x=>x.CentreSanctions).Include(x => x.Centres)
                    .Include(x => x.FacilitySettings).Include(x => x.Offices)
                    .Include(x => x.PinHistories).Include(x => x.Pins)
                    .Include(x => x.SchoolFacilities).Include(x => x.SchoolPayments)
                    .Include(x => x.SchoolStaffDegrees) as IQueryable<ApplicationUsers>;

                //Search by
                if (!string.IsNullOrWhiteSpace(usersResourceParameters.EmailAddress))
                {
                    var emailAddress = usersResourceParameters.EmailAddress.Trim();
                    collection = collection.Where(c => c.EmailAddress == (emailAddress));
                }

                //Search by
                if (!string.IsNullOrWhiteSpace(usersResourceParameters.SearchQuery))
                {
                    var searchQuery = usersResourceParameters.SearchQuery.Trim();
                    collection = collection.Where(c => c.EmailAddress.Contains(searchQuery)
                    || c.Surname.Contains(searchQuery)
                    || c.Othernames.Contains(searchQuery));
                }

                //Order by
                if (!string.IsNullOrWhiteSpace(usersResourceParameters.OrderBy))
                {
                    //get property mapping
                    var usersPropertyMappingDictionary =
                        _propertyMappingService.GetPropertyMapping<AccountsDto, ApplicationUsers>();

                    collection = collection.ApplySort(usersResourceParameters.OrderBy,
                        usersPropertyMappingDictionary);
                }

                //Get the mapped data
                var mappingData = (_mapper.Map<IEnumerable<AccountsDto>>(collection)).AsQueryable();// as IQueryable<AccountsDto>;

                //return the paginated data
                return PagedList<AccountsDto>.Create(mappingData, usersResourceParameters.PageNumber, usersResourceParameters.PageSize);
            });
        }

        public async Task<IEnumerable<AccountsDto>> GetRoleAccounts(Guid roleId)
        {
            return await Task.Run(async () =>
            {
                var result = await _context.ApplicationUsers.Where(c => c.RoleId == roleId).ToListAsync<ApplicationUsers>();
                return _mapper.Map<IEnumerable<AccountsDto>>(result);
            });
        }


        public async Task<bool> Save()
        {
            return await Task.Run(async () =>
            {
                return (await _context.SaveChangesAsync() >= 0);
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
