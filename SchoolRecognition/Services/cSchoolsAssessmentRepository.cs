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
    public class cSchoolsAssessment: ControllerBase, ISchoolsAssessmentRepository, IDisposable
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public cSchoolsAssessment(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
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

        // create new facility items


       public async Task<FacilityItemsDto> Create(FacilityItemsCreateDto create)
        {
            return await Task.Run(async () =>
            {
                if (create == null)
                {
                    throw new ArgumentNullException(nameof(create));
                }
                var facilityEntity = _mapper.Map<FacilityItems>(create);

                facilityEntity.Id = Guid.NewGuid();

                _context.FacilityItems.Add(facilityEntity);

                //call the save method
                bool saveResult = await Save();

                return _mapper.Map<FacilityItemsDto>(facilityEntity);
            });
        }

        // Update Facility items

        public async Task<FacilityItemsDto> Update(Guid facSettingsID, FacilityItemsUpdateDto update)
        {
            return await Task.Run(async () =>
            {
                if (update == null)
                {
                    throw new ArgumentNullException(nameof(update));
                }

                var facResult = await _context.FacilityItems.Include(f => f.FacilitySettings).FirstOrDefaultAsync(c => c.Id == facSettingsID);

                if (facResult == null)
                {
                    throw new ArgumentNullException(nameof(facResult));
                }
                var val = _mapper.Map(update, facResult);

                //val.Name = categories.Name;
                //val.Code = categories.Code;
                _context.FacilityItems.Update(val);
                bool save = await Save();

                return _mapper.Map<FacilityItemsDto>(val);
            });
        }


        //get all facility items
        public async Task<IEnumerable<FacilityItemsDto>> List()
        {
            return await Task.Run(async () =>
            {
                var result = await _context.FacilityItems.Include(c => c.FacilitySettings).ToListAsync();
                return _mapper.Map<IEnumerable<FacilityItemsDto>>(result);

            });
        }


        //List all facility items by ID

        public async Task<FacilityItemsDto> ListById(Guid listByID)
        {
            return await Task.Run(async () =>
            {
                if (listByID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(listByID));
                }

                var result = await _context.FacilityItems.Include(f => f.FacilitySettings).FirstOrDefaultAsync(c => c.Id == listByID);
                //return the mapped object
                return _mapper.Map<FacilityItemsDto>(result);
            });
        }



        //partial update of Facility items
        public async Task<FacilityItemsDto> Patch(Guid facSettingsID, JsonPatchDocument<FacilityItemsUpdateDto> patchDocument)
        {
            throw new NotImplementedException();
        }


        //delete facility item
        public async Task<FacilityItemsDto> Delete(Guid delete)
        {
            return await Task.Run(async () =>
            {
                //var user = await GetSchoolsById(schoolId);
                var item = await _context.FacilityItems.FirstOrDefaultAsync(c => c.Id == delete);
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(delete));
                }

                _context.Remove(item);
                await Save();

                return _mapper.Map<FacilityItemsDto>(item);
            });
        }

        public async Task<bool> FacilityItemsExists(Guid ItemsID)
        {
            return await Task.Run(async () =>
            {
                if (ItemsID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(ItemsID));
                }

                return await _context.FacilityItems.AnyAsync(a => a.Id == ItemsID);
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
      


   
      public async  Task<PagedList<FacilityItemsDto>> List(FacilityItemsResourceParams itemsResourceParams)
        {
            return await Task.Run(async () =>
            {
                if (itemsResourceParams == null)
                {
                    throw new ArgumentNullException(nameof(itemsResourceParams));
                }
                //instantiate return value
                PagedList<FacilityItemsDto> returnValue = PagedList<FacilityItemsDto>
                            .Create(Enumerable.Empty<FacilityItemsDto>().AsQueryable(),
                                itemsResourceParams.PageNumber,
                                itemsResourceParams.PageSize);

                //cast the collection into an IQueryable object 
                //search the record
                var collection = _context.FacilityItems
                    //.Include(x => x.FacilitySettings)
                    //.ThenInclude(x => x.Specification)
                     as IQueryable<FacilityItems>;

                //Search
                //if (!string.IsNullOrWhiteSpace(resourceParameters.SearchQuery))
                //{

                //    var searchQuery = resourceParameters.SearchQuery.Trim().ToUpper();

                //    collection = collection.Where(a => a.DepartmentTitle.ToUpper().Contains(searchQuery)
                //    || a.ShortName.ToUpper().Contains(searchQuery)
                //    );
                //}


                //var collection = _context.FacilityItems.Include(x => x.FacilitySettings)
                //    as IQueryable<FacilityItems>;



                //Search by
                if (!string.IsNullOrWhiteSpace(itemsResourceParams.SearchQuery))
                {
                    var searchQuery = itemsResourceParams.SearchQuery.Trim();
                    collection = collection.Where(c => c.Description.ToUpper().Contains(searchQuery));
                }

                //Order by
                if (!string.IsNullOrWhiteSpace(itemsResourceParams.OrderBy))
                {
                    //get property mapping
                    var usersPropertyMappingDictionary =
                        _propertyMappingService.GetPropertyMapping<FacilityItemsDto, FacilityItems>();

                    collection = collection.ApplySort(itemsResourceParams.OrderBy,
                        usersPropertyMappingDictionary);
                }

                //Get the mapped data
                var mappingData = (_mapper.Map<IEnumerable<FacilityItemsDto>>(collection)).AsQueryable();// as IQueryable<AccountsDto>;

                //return the paginated data
                return PagedList<FacilityItemsDto>.Create(mappingData, itemsResourceParams.PageNumber, itemsResourceParams.PageSize);
            });
        }

        Task<IEnumerable<SchoolsAssessmentDto>> ISchoolsAssessmentRepository.List()
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<SchoolsAssessmentDto>> List(SchoolsAssessmentResourceParams itemsResourceParams)
        {
            throw new NotImplementedException();
        }

        public Task<SchoolsAssessmentDto> Create(SchoolsAssessmentCreateDto create)
        {
            throw new NotImplementedException();
        }

        public Task<SchoolsAssessmentDto> Update(Guid facSettingsID, SchoolsAssessmentUpdateDto update)
        {
            throw new NotImplementedException();
        }

        Task<SchoolsAssessmentDto> ISchoolsAssessmentRepository.ListById(Guid listByID)
        {
            throw new NotImplementedException();
        }

        public Task<SchoolsAssessmentDto> Patch(Guid facSettingsID, JsonPatchDocument<SchoolsAssessmentUpdateDto> patchDocument)
        {
            throw new NotImplementedException();
        }

        Task<SchoolsAssessmentDto> ISchoolsAssessmentRepository.Delete(Guid delete)
        {
            throw new NotImplementedException();
        }
    }
}
