using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class cSchoolClassesRepository: ControllerBase, ISchoolClassesRepository, IDisposable
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public cSchoolClassesRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
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

        public async Task<IEnumerable<SchoolClassesDto>> List()
        {
            return await Task.Run(async () =>
            {
                var result = await _context.SchoolClasses.Include( s => s.Class).Include(s => s.School).ToListAsync<SchoolClasses>();
                return _mapper.Map<IEnumerable<SchoolClassesDto>>(result);

            });
        }

        public async Task<SchoolClassesDto> Create(CreateSchoolClassesDto createSchool)
        {
            return await Task.Run(async () =>
            {
                if (createSchool == null)
                {
                    throw new ArgumentNullException(nameof(createSchool));
                }
                var schClassEntity = _mapper.Map<SchoolClasses>(createSchool);
                schClassEntity.Id = Guid.NewGuid();

                _context.SchoolClasses.Add(schClassEntity);

                //call the save method
                bool saveResult = await Save();

                return _mapper.Map<SchoolClassesDto>(schClassEntity);
            });
        }

        public async Task<SchoolClassesDto> Update(Guid schoolClassID, UpdateSchoolClassesDto updateSchool)
        {
            return await Task.Run(async () =>
            {
                if (updateSchool == null)
                {
                    throw new ArgumentNullException(nameof(updateSchool));
                }

                var schClassResult = await _context.Ranks.FirstOrDefaultAsync(c => c.Id == schoolClassID);

                if (schClassResult == null)
                {
                    throw new ArgumentNullException(nameof(schClassResult));
                }
                var val = _mapper.Map(updateSchool, schClassResult);

               
                _context.Ranks.Update(val);
                bool save = await Save();

                return _mapper.Map<SchoolClassesDto>(val);
            });
        }

        
        public async Task<SchoolClassesDto> ListById(Guid id)
        {
            return await Task.Run(async () =>
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var result = await _context.SchoolClasses.Include(x=>x.School).Include(x=>x.Class).
                FirstOrDefaultAsync(c => c.Id == id);
                //return the mapped object
                return _mapper.Map<SchoolClassesDto>(result);
            });
        }

        public async Task<SchoolClassesDto> Patch(Guid schoolClassID, JsonPatchDocument<UpdateSchoolClassesDto> patchDocument)
        {
            return await Task.Run(async () =>
            {
                if (patchDocument == null)
                {
                    throw new ArgumentNullException(nameof(patchDocument));
                }

                var schClassResult = await _context.SchoolClasses.FirstOrDefaultAsync(c => c.Id == schoolClassID);

                if (schClassResult == null)
                {
                    throw new ArgumentNullException(nameof(schClassResult));
                }

                //map the extracted result with the Update class
                var schClassToPatch = _mapper.Map<UpdateSchoolClassesDto>(schClassResult);

                //apply the patch where there are changes and resolve error
                patchDocument.ApplyTo(schClassToPatch, ModelState);



                if (!TryValidateModel(schClassToPatch))
                {
                    throw new ArgumentNullException(nameof(patchDocument));
                    //return ValidationProblem(ModelState);
                }

                //map back the patched record to the previously extracted record from DB
                _mapper.Map(schClassToPatch, schClassResult);

                bool save = await Save();

                return _mapper.Map<SchoolClassesDto>(schClassResult);
            });
        }

        public  async Task<SchoolClassesDto> Delete(Guid schoolClassID)
        {
            return await Task.Run(async () =>
            {
                var schClassResult = await _context.SchoolClasses.FirstOrDefaultAsync(c => c.Id == schoolClassID);
                if (schClassResult == null)
                {
                    throw new ArgumentNullException(nameof(schoolClassID));
                }
                // await _context.SaveChangesAsync();
                _context.SchoolClasses.Remove(schClassResult);
                await Save();

                return _mapper.Map<SchoolClassesDto>(schClassResult);
            });
        }

        public async Task<bool> SchoolClassesExists(Guid schoolClassID)
        {
            return await Task.Run(async () =>
            {
                if (schoolClassID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(schoolClassID));
                }

                return await _context.SchoolClasses.AnyAsync(a => a.Id == schoolClassID);
            });
        }


        public  Task<IEnumerable<SchoolClassesDto>> GetAllSchoolClassesAllocations(Guid id)
        {
            throw new NotImplementedException();
            //return await Task.Run(async () =>
            //{


            ////var customersFromCity = _context.SchoolClasses.Where(s => s.Id == id);

            ////    IQueryable<SchoolClassesDto> customersDTO =
            ////        customersFromCity.ProjectTo<SchoolClassesDto>();

            //    var result = await _context.SchoolClasses.Where(s => s.Id == id).ProjectTo<SchoolClassesDto>().ToListAsync();
            //    return _mapper.Map<SchoolClassesDto>(result);


            //});
        }

        public   Task<IEnumerable<SchoolClassesDto>> GetAllSchoolClassesSchoolSubjectStaffs(Guid id)
        {

            throw new NotImplementedException();
        }

        public async Task<PagedList<SchoolClassesDto>> List(SchoolClassesResourceParams resourceParams)
        {
            return await Task.Run(async () =>
            {
                if (resourceParams == null)
                {
                    throw new ArgumentNullException(nameof(resourceParams));
                }


                var collection = _context.SchoolClasses.Include(x => x.School).Include(x=>x.Class)
                     as IQueryable<SchoolClasses>;



                //Search by
                if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                {
                    var searchQuery = resourceParams.SearchQuery.Trim();
                    collection = collection.Where(c => c.TotalStudents.ToString().Contains(searchQuery));
                }

                //Order by
                if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                {
                    //get property mapping
                    var usersPropertyMappingDictionary =
                        _propertyMappingService.GetPropertyMapping<SchoolClassesDto, SchoolClasses>();

                    collection = collection.ApplySort(resourceParams.OrderBy,
                        usersPropertyMappingDictionary);
                }

                //Get the mapped data
                var mappingData = (_mapper.Map<IEnumerable<SchoolClassesDto>>(collection)).AsQueryable();// as IQueryable<AccountsDto>;

                //return the paginated data
                return PagedList<SchoolClassesDto>.Create(mappingData, resourceParams.PageNumber, resourceParams.PageSize);
            });


        }

        public Task<PagedList<SchoolClassesDto>> GetAllSchoolClassesAllocationsAsPagedList(Guid id, SchoolClassesAllocationsResourceParams resourceParams)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<SchoolClassesDto>> GetAllSchoolClassesSchSubStaffsAsPagedList(Guid id, SchoolClassesSchSubStaffsResourceParams resourceParams)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SchoolClassesExists(string schoolClassID)
        {
            throw new NotImplementedException();
        }

        public Task<SchoolClassesDependencyDto> GetSchoolClassCreateDepedencys()
        {
            throw new NotImplementedException();
        }
    }
}
