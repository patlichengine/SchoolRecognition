
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.DbContexts;
using SchoolRecognition.Entities;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SchoolRecognition.Services
{
    public class cClassSettingsRepository : IClassSettingsRepository, IDisposable
    {

        //private readonly ConnectionString _connectionString;
        //private IPinsRepository _pinService;
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;
       

        //public cRecognitionTypesRepository(ConnectionString connectionString)
        //{
        //    //_connectionString = connectionString;
        //    //_pinService = new cPinsRepository(_connectionString);

        //}

        public cClassSettingsRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
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
        public async Task<ClassSettingsDto> Create(ClassSettingsCreateDto create)
        {
            return await Task.Run(async () =>
            {
                if (create == null)
                {
                    throw new ArgumentNullException(nameof(create));
                }
                var classSettings = _mapper.Map<ClassSettings>(create);

                classSettings.Id = Guid.NewGuid();
                //classSettings.Name = create.Name;

                _context.ClassSettings.Add(classSettings);

                //call the save method
                bool saveResult = await Save();

                return _mapper.Map<ClassSettingsDto>(classSettings);
            });
        }

        public async Task<bool> Save()
        {
            return await Task.Run(async () =>
            {
                return (await _context.SaveChangesAsync() >= 0);
            });
        }
       
        public async Task<ClassSettingsDto> Delete(Guid delete)
        {
            return await Task.Run(async () =>
            {
                //var user = await GetSchoolsById(schoolId);
                var item = await _context.ClassSettings.FirstOrDefaultAsync(c => c.Id == delete);
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(delete));
                }

                _context.Remove(item);
                await Save();

                return _mapper.Map<ClassSettingsDto>(item);
            });
        }
        
        public async Task<bool> ClassSettingsExists(Guid ItemsID)
        {
            return await Task.Run(async () =>
            {
                if (ItemsID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(ItemsID));
                }

                return await _context.ClassSettings.AnyAsync(a => a.Id == ItemsID);
            });
        }

        public async Task<PagedList<ClassSettingsDto>> List(ClassSettingsResourceParams itemsResourceParams)
        {
            return await Task.Run(async () =>
            {
                if (itemsResourceParams == null)
                {
                    throw new ArgumentNullException(nameof(itemsResourceParams));
                }
                //instantiate return value
                PagedList<ClassSettingsDto> returnValue = PagedList<ClassSettingsDto>
                            .Create(Enumerable.Empty<ClassSettingsDto>().AsQueryable(),
                                itemsResourceParams.PageNumber,
                                itemsResourceParams.PageSize);

                //cast the collection into an IQueryable object 
                //search the record
                var collection = _context.ClassSettings
                     .Include(x => x.SchoolClasses).Include(y => y.SchoolStaffSubjects)
                     //.ThenInclude(x => x.Specification)
                     as IQueryable<ClassSettings>;

                

                //Search by
                if (!string.IsNullOrWhiteSpace(itemsResourceParams.SearchQuery))
                {
                    var searchQuery = itemsResourceParams.SearchQuery.Trim();
                    collection = collection.Where(c => c.Name.ToUpper().Contains(searchQuery));
                }

                //Order by
                if (!string.IsNullOrWhiteSpace(itemsResourceParams.OrderBy))
                {
                    //get property mapping
                    var usersPropertyMappingDictionary =
                        _propertyMappingService.GetPropertyMapping<ClassSettingsDto, ClassSettings>();

                    collection = collection.ApplySort(itemsResourceParams.OrderBy,
                        usersPropertyMappingDictionary);
                }

                //Get the mapped data
                var mappingData = (_mapper.Map<IEnumerable<ClassSettingsDto>>(collection)).AsQueryable();// as IQueryable<AccountsDto>;

                //return the paginated data
                return PagedList<ClassSettingsDto>.Create(mappingData, itemsResourceParams.PageNumber, itemsResourceParams.PageSize);
            });
        }

        public async Task<ClassSettingsDto> ListById(Guid listByID)
        {
            return await Task.Run(async () =>
            {
                if (listByID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(listByID));
                }

                var result = await _context.ClassSettings.Include(f => f.SchoolClasses)
                                                        .Include(f =>f.SchoolStaffSubjects).FirstOrDefaultAsync(c => c.Id == listByID);
                //return the mapped object
                return _mapper.Map<ClassSettingsDto>(result);
            });
        }

        public Task<ClassSettingsDto> Patch(Guid classSettingID, JsonPatchDocument<ClassSettingsUpdateDto> patchDocument)
        {
            throw new NotImplementedException();
        }

        public async Task<ClassSettingsDto> Update(Guid classSettingID, ClassSettingsUpdateDto update)
        {
            return await Task.Run(async () =>
            {
                if (update == null)
                {
                    throw new ArgumentNullException(nameof(update));
                }

                var classSettingResult = await _context.ClassSettings.Include(f => f.SchoolClasses)
                                                            .Include(f => f.SchoolClasses).FirstOrDefaultAsync(c => c.Id == classSettingID);

                if (classSettingResult == null)
                {
                    throw new ArgumentNullException(nameof(classSettingResult));
                }
                var val = _mapper.Map(update, classSettingResult);

                //val.Name = categories.Name;
                //val.Code = categories.Code;
                _context.ClassSettings.Update(val);
                bool save = await Save();

                return _mapper.Map<ClassSettingsDto>(val);
            });
        }

    }
}
