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
    public class cFacilityTypesRepository: ControllerBase, IFacilityTypesRepository, IDisposable
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public cFacilityTypesRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService;
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

        public async Task<IEnumerable<FacilityTypesDto>> List()
        {
            return await Task.Run(async () =>
            {
                var result = await _context.FacilityTypes.ToListAsync<FacilityTypes>();
                return _mapper.Map<IEnumerable<FacilityTypesDto>>(result);

            });
        }

        public async Task<FacilityTypesDto> Create(CreateFacilityTypesDto createFacility)
        {
            return await Task.Run(async () =>
            {
                if (createFacility == null)
                {
                    throw new ArgumentNullException(nameof(createFacility));
                }
                var facilityEntity = _mapper.Map<FacilityTypes>(createFacility);
                facilityEntity.Id = Guid.NewGuid();

                _context.FacilityTypes.Add(facilityEntity);

                //call the save method
                bool saveResult = await Save();

                return _mapper.Map<FacilityTypesDto>(facilityEntity);
            });
        }

        public async Task<FacilityTypesDto> Update(Guid facTypesID, UpdateFacilityTypesDto updateFacility)
        {
            return await Task.Run(async () =>
            {
                if (updateFacility == null)
                {
                    throw new ArgumentNullException(nameof(updateFacility));
                }

                var facResult = await _context.FacilityTypes.FirstOrDefaultAsync(c => c.Id == facTypesID);

                if (facResult == null)
                {
                    throw new ArgumentNullException(nameof(facResult));
                }
                var val = _mapper.Map(updateFacility, facResult);

                //val.Name = categories.Name;
                //val.Code = categories.Code;
                _context.FacilityTypes.Update(val);
                bool save = await Save();

                return _mapper.Map<FacilityTypesDto>(val);
            });
            
          }

        public async Task<FacilityTypesDto> ListById(Guid facTypesID)
        {
            return await Task.Run(async () =>
            {
                if (facTypesID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(facTypesID));
                }

                var result = await _context.FacilityTypes.FirstOrDefaultAsync(c => c.Id == facTypesID);
                //return the mapped object
                return _mapper.Map<FacilityTypesDto>(result);
            });
        }

        public async Task<FacilityTypesDto> Patch(Guid facTypesID, JsonPatchDocument<UpdateFacilityTypesDto> patchDocument)
        {
            return await Task.Run(async () =>
            {
                if (patchDocument == null)
                {
                    throw new ArgumentNullException(nameof(patchDocument));
                }

                var facResult = await _context.FacilityTypes.FirstOrDefaultAsync(c => c.Id == facTypesID);

                if (facResult == null)
                {
                    throw new ArgumentNullException(nameof(facResult));
                }

                //map the extracted result with the Update class
                var facToPatch = _mapper.Map<UpdateFacilityTypesDto>(facResult);

                //apply the patch where there are changes and resolve error
                patchDocument.ApplyTo(facToPatch, ModelState);



                if (!TryValidateModel(facToPatch))
                {
                    throw new ArgumentNullException(nameof(patchDocument));
                    //return ValidationProblem(ModelState);
                }

                //map back the patched record to the previously extracted record from DB
                _mapper.Map(facToPatch, facResult);

                bool save = await Save();

                return _mapper.Map<FacilityTypesDto>(facResult);
            });
        }

        public async Task<FacilityTypesDto> Delete(Guid facTypesID)
        {
            return await Task.Run(async () =>
            {
                var facResult = await _context.FacilityTypes.FirstOrDefaultAsync(c => c.Id == facTypesID);
                if (facResult == null)
                {
                    throw new ArgumentNullException(nameof(facTypesID));
                }
                // await _context.SaveChangesAsync();
                _context.FacilityTypes.Remove(facResult);
                await Save();

                return _mapper.Map<FacilityTypesDto>(facResult);
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

        public async Task<FacilityTypesDto> CreateFacilityTypes(Guid FacSettingsID, CreateFacilityTypesDto createFacility)
        {
            return await Task.Run(async () =>
            {
                if (FacSettingsID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(FacSettingsID));
                }
                if (createFacility == null)
                {
                    throw new ArgumentNullException(nameof(createFacility));
                }
                var facilityEntity = _mapper.Map<FacilityTypes>(createFacility);

                facilityEntity.Id = FacSettingsID;

                _context.FacilityTypes.Add(facilityEntity);

                //call the save method
                bool saveResult = await Save();

                return _mapper.Map<FacilityTypesDto>(facilityEntity);
            });
        }

        public Task<FacilityTypesDto> GetFacilitySectTypesById(Guid FacSettingsID, Guid facTypesID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FacilitySecExists(Guid FacSettingsID)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<FacilityTypesDto>> List(FacilityTypesResourceParams resourceParams)
        {
            return await Task.Run(async () =>
            {
                if (resourceParams == null)
                {
                    throw new ArgumentNullException(nameof(resourceParams));
                }


                var collection = _context.FacilityTypes.Include(x => x.FacilitySettings)
                    as IQueryable<FacilityTypes>;



                //Search by
                if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                {
                    var searchQuery = resourceParams.SearchQuery.Trim();
                    collection = collection.Where(c => c.Title.Contains(searchQuery));
                }

                //Order by
                if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                {
                    //get property mapping
                    var usersPropertyMappingDictionary =
                        _propertyMappingService.GetPropertyMapping<FacilityTypesDto, FacilityTypes>();

                    collection = collection.ApplySort(resourceParams.OrderBy,
                        usersPropertyMappingDictionary);
                }

                //Get the mapped data
                var mappingData = (_mapper.Map<IEnumerable<FacilityTypesDto>>(collection)).AsQueryable();// as IQueryable<AccountsDto>;

                //return the paginated data
                return PagedList<FacilityTypesDto>.Create(mappingData, resourceParams.PageNumber, resourceParams.PageSize);
            });
        }
    }
}
