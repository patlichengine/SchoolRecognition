
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
    public class cLocationRepository : ControllerBase, ILocationRepository, IDisposable
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public cLocationRepository(SchoolRecognitionContext  context, IMapper mapper, IPropertyMappingService propertyMapping)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMapping;
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

       
        // save category
        public async Task<bool> Save()
        {
            return await Task.Run(async () =>
            {
                return (await _context.SaveChangesAsync() >= 0);
            });

        }

     


        public async Task<LocationDto> Patch(Guid userId, JsonPatchDocument<UpdateLocationDto> patchDocument)
        {
            return await Task.Run(async () =>
            {
                if (patchDocument == null)
                {
                    throw new ArgumentNullException(nameof(patchDocument));
                }

                var user = await _context.LocationTypes.FirstOrDefaultAsync(c => c.Id == userId);

                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                //map the extracted result with the Update class
                var userToPatch = _mapper.Map<UpdateLocationDto>(user);

                //apply the patch where there are changes and resolve error
                patchDocument.ApplyTo(userToPatch, ModelState);

               

                if (!TryValidateModel(userToPatch))
                {
                    throw new ArgumentNullException(nameof(patchDocument));
                    //return ValidationProblem(ModelState);
                }

                //map back the patched record to the previously extracted record from DB
                _mapper.Map(userToPatch, user);

                bool save = await Save();

                return _mapper.Map<LocationDto>(user);
            });
        }

        public override ActionResult ValidationProblem(
            [ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices
                .GetRequiredService<IOptions<ApiBehaviorOptions>>();

            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);

        }

        public async Task<LocationDto> ListById(Guid id)
        {
            return await Task.Run(async () =>
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var result = await _context.LocationTypes.FirstOrDefaultAsync(c => c.Id == id);
                //return the mapped object
                return _mapper.Map<LocationDto>(result);
            });
        }

        public async Task<IEnumerable<LocationDto>> List()
        {
            return await Task.Run(async () =>
            {
                var result = await _context.LocationTypes.ToListAsync<LocationTypes>();
                return _mapper.Map<IEnumerable<LocationDto>>(result);

            });
        }

        public async Task<LocationDto> Create(CreateLocationDto locations)
        {
            return await Task.Run(async () =>
            {
                if (locations == null)
                {
                    throw new ArgumentNullException(nameof(locations));
                }
                var locationEntity = _mapper.Map<LocationTypes>(locations);
                locationEntity.Id = Guid.NewGuid();
                
                _context.LocationTypes.Add(locationEntity);

                //call the save method
                bool saveResult = await Save();

                return _mapper.Map<LocationDto>(locationEntity);
            });
        }

        public async Task<LocationDto> Update(Guid id, UpdateLocationDto updateLocationDto)
        {
            return await Task.Run(async () =>
            {
                if (updateLocationDto == null)
                {
                    throw new ArgumentNullException(nameof(updateLocationDto));
                }

                var user = await _context.LocationTypes.FirstOrDefaultAsync(c => c.Id == id);

                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }
                var val = _mapper.Map(updateLocationDto, user);

                //val.Name = categories.Name;
                //val.Code = categories.Code;
                _context.LocationTypes.Update(val);
                bool save = await Save();

                return _mapper.Map<LocationDto>(val);
            });
        }

       

        public async Task<bool> LocationsExists(Guid locationId)
        {
            return await Task.Run(async () =>
            {
                if (locationId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(locationId));
                }

                return await _context.LocationTypes.AnyAsync(a => a.Id == locationId);
            });
        }

        public async Task<LocationDto> Delete(Guid locationId)
        {
            return await Task.Run(async () =>
            {
                var locations = await _context.LocationTypes.FirstOrDefaultAsync(c => c.Id == locationId);
                if (locations == null)
                {
                    throw new ArgumentNullException(nameof(locationId));
                }
                // await _context.SaveChangesAsync();
                _context.LocationTypes.Remove(locations);
                await Save();

                return _mapper.Map<LocationDto>(locations);
            });
        }

        public async Task<PagedList<LocationDto>> List(LocationTypesResourceParams resourceParams)
        {
            return await Task.Run(async () =>
            {
                if (resourceParams == null)
                {
                    throw new ArgumentNullException(nameof(resourceParams));
                }


                var collection = _context.LocationTypes
                     as IQueryable<LocationTypes>;



                //Search by
                if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                {
                    var searchQuery = resourceParams.SearchQuery.Trim();
                    collection = collection.Where(c => c.Name.Contains(searchQuery)
                    || c.Description.Contains(searchQuery));
                }

                //Order by
                if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                {
                    //get property mapping
                    var usersPropertyMappingDictionary =
                        _propertyMappingService.GetPropertyMapping<LocationDto, LocationTypes>();

                    collection = collection.ApplySort(resourceParams.OrderBy,
                        usersPropertyMappingDictionary);
                }

                //Get the mapped data
                var mappingData = (_mapper.Map<IEnumerable<LocationDto>>(collection)).AsQueryable();// as IQueryable<AccountsDto>;

                //return the paginated data
                return PagedList<LocationDto>.Create(mappingData, resourceParams.PageNumber, resourceParams.PageSize);
            });
        }
    }
}
