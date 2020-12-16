using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
    public class cFacilitySettingsRepository: ControllerBase, IFacilitySettingsRepository, IDisposable
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public cFacilitySettingsRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
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
     

   

        public async Task<bool> Save()
        {
            return await Task.Run(async () =>
            {
                return (await _context.SaveChangesAsync() >= 0);
            });
        }

     


        public async Task<IEnumerable<FacilitySettingsDto>> List()
        {
            return await Task.Run(async () =>
            {
                var result = await _context.FacilitySettings.Include(c => c.FacilityType).ToListAsync();
                return _mapper.Map<IEnumerable<FacilitySettingsDto>>(result);

            });
        }

        public async Task<FacilitySettingsDto> Create(CreateFacilitySettingsDto createFacSettings)
        {
            return await Task.Run(async () =>
            {
                if (createFacSettings == null)
                {
                    throw new ArgumentNullException(nameof(createFacSettings));
                }
                var facilityEntity = _mapper.Map<FacilitySettings>(createFacSettings);
                //facilityEntity.CreatedBy = facilityEntity.CreatedByNavigation.Id;
                facilityEntity.DateCreated = DateTime.Now;
                facilityEntity.Id = Guid.NewGuid();

                _context.FacilitySettings.Add(facilityEntity);

                //call the save method
                bool saveResult = await Save();

                return _mapper.Map<FacilitySettingsDto>(facilityEntity);
            });
        }

        public async Task<FacilitySettingsDto> Update(Guid facSettingsID, UpdateFacilitySettingsDto updateFacSettings)
        {
            return await Task.Run(async () =>
            {
                if (updateFacSettings == null)
                {
                    throw new ArgumentNullException(nameof(updateFacSettings));
                }

                var facResult = await _context.FacilitySettings.Include(f => f.FacilityType).FirstOrDefaultAsync(c => c.Id == facSettingsID);

                if (facResult == null)
                {
                    throw new ArgumentNullException(nameof(facResult));
                }
                var val = _mapper.Map(updateFacSettings, facResult);

                //val.Name = categories.Name;
                //val.Code = categories.Code;
                _context.FacilitySettings.Update(val);
                bool save = await Save();

                return _mapper.Map<FacilitySettingsDto>(val);
            });
        }

        public async Task<FacilitySettingsDto> ListById(Guid facSettingsID)
        {
            return await Task.Run(async () =>
            {
                if (facSettingsID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(facSettingsID));
                }

                var result = await _context.FacilitySettings.Include(f => f.FacilityType).FirstOrDefaultAsync(c => c.Id == facSettingsID);
                //return the mapped object
                return _mapper.Map<FacilitySettingsDto>(result);
            });
        }

        public Task<FacilitySettingsDto> Patch(Guid facSettingsID, JsonPatchDocument<FacilityItemsUpdateDto> patchDocument)
        {
            throw new NotImplementedException();
        }

        public async Task<FacilitySettingsDto> Delete(Guid facSettingsID)
        {
            return await Task.Run(async () =>
            {
                //var user = await GetSchoolsById(schoolId);
                var user = await _context.FacilitySettings.FirstOrDefaultAsync(c => c.Id == facSettingsID);
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(facSettingsID));
                }

                _context.Remove(user);
                await Save();

                return _mapper.Map<FacilitySettingsDto>(user);
            });
        }

        public async Task<bool> FacilitySettingsExists(Guid facSettingsID)
        {
            return await Task.Run(async () =>
            {
                if (facSettingsID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(facSettingsID));
                }

                return await _context.FacilitySettings.AnyAsync(a => a.Id == facSettingsID);
            });
        }

        public async Task<IEnumerable<FacilityTypesDto>> GetAllFacilityTypes()
        {
            return await Task.Run(async () =>
            {
                var result = await _context.FacilityTypes.ToListAsync();
                return _mapper.Map<IEnumerable<FacilityTypesDto>>(result);

            });
        }

        // Facility Items
        public async Task<IEnumerable<FacilityItemsDto>> GetAllFacilityItems()
        {
            return await Task.Run(async () =>
            {
                var result = await _context.FacilityItems.ToListAsync();
                return _mapper.Map<IEnumerable<FacilityItemsDto>>(result);

            });
        }
        //get subjects list
        public async Task<IEnumerable<SubjectsViewDto>> GetAllSubjects()
        {
            return await Task.Run(async () =>
            {

                var result = await _context.Subjects.ToListAsync();
                return _mapper.Map<IEnumerable<SubjectsViewDto>>(result);

            });
        }

        //gets the settings of a facility
        public async Task<FacilitySettingsDto> GetSettingsForAFacType(Guid facType, Guid facSetID)
        {
            return await Task.Run(async () =>
            {
                if (facType == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(facType));
                }
                if (facSetID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(facSetID));
                }

                var result = await _context.FacilitySettings.Where(f=>f.FacilityTypeId == facType && f.Id ==facSetID ).FirstOrDefaultAsync();
                //return the mapped object
                return _mapper.Map<FacilitySettingsDto>(result);

            });
        }

        public async Task<FacilitySettingsDto> CreateFacilitySecTypes(Guid FacSettingsID, CreateFacilitySettingsDto createFacility)
        {
            return await Task.Run(async () =>
            {
                if(FacSettingsID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(FacSettingsID));

                }
                if (createFacility == null)
                {
                    throw new ArgumentNullException(nameof(createFacility));
                }
                var facilityEntity = _mapper.Map<FacilitySettings>(createFacility);
                //facilityEntity.CreatedBy = facilityEntity.CreatedByNavigation.Id;
                facilityEntity.FacilityTypeId = FacSettingsID;
                facilityEntity.DateCreated = DateTime.Now;
                facilityEntity.Id = Guid.NewGuid();

                _context.FacilitySettings.Add(facilityEntity);

                //call the save method
                bool saveResult = await Save();

                return _mapper.Map<FacilitySettingsDto>(facilityEntity);
            });
        }

        public async Task<FacilitySettingsDto> GetFacilitySectTypesById(Guid FacSettingsID, Guid facTypesID)
        {
            return await Task.Run(async () =>
            {
                if (FacSettingsID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(FacSettingsID));

                }
                if (facTypesID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(facTypesID));

                }

                var result = await _context.FacilitySettings.Where(f => f.Id == FacSettingsID && f.FacilityType.Id == facTypesID).FirstOrDefaultAsync();

                return _mapper.Map<FacilitySettingsDto>(result);
            });
           

        }

        public async Task<bool> FacilityTypesExists(Guid facTypesID)
        {
            return await Task.Run(async () =>
            {
                if (facTypesID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(facTypesID));
                }

                return await _context.FacilityTypes.AnyAsync(a => a.Id == facTypesID);
            });
        }

        public async Task<IEnumerable<FacilitySettingsDto>> GetSettingsForFacType(Guid facTypeID)
        {
            return await Task.Run(async () => {

                if (facTypeID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(facTypeID));
                }

                var result = await _context.FacilitySettings
                            .Where(c => c.FacilityTypeId == facTypeID).OrderBy(c => c.ItemNo).ToListAsync();

                return _mapper.Map<IEnumerable<FacilitySettingsDto>>(result);
            });
        }

        public async Task<PagedList<FacilitySettingsDto>> List(FacilitySettingsResourceParams resourceParams)
        {
            return await Task.Run(async () =>
            {
                if (resourceParams == null)
                {
                    throw new ArgumentNullException(nameof(resourceParams));
                }


                var collection = _context.FacilitySettings.Include(x => x.FacilityType).Include(x=>x.FacilityItem)
                    .Include(x => x.CreatedByNavigation).Include(x=>x.Subject) as IQueryable<FacilitySettings>;



                //Search by
                if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                {
                    var searchQuery = resourceParams.SearchQuery.Trim();
                    collection = collection.Where(c => c.Specification.Contains(searchQuery));
                }

                //Order by
                if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                {
                    //get property mapping
                    var usersPropertyMappingDictionary =
                        _propertyMappingService.GetPropertyMapping<FacilitySettingsDto, FacilitySettings>();

                    collection = collection.ApplySort(resourceParams.OrderBy,
                        usersPropertyMappingDictionary);
                }

                //Get the mapped data
                var mappingData = (_mapper.Map<IEnumerable<FacilitySettingsDto>>(collection)).AsQueryable();// as IQueryable<AccountsDto>;

                //return the paginated data
                return PagedList<FacilitySettingsDto>.Create(mappingData, resourceParams.PageNumber, resourceParams.PageSize);
            });
        }

      
    }
}
