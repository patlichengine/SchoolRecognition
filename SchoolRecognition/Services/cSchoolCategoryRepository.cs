
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
    public class cSchoolCategoryRepository : ControllerBase, ISchoolCategoryRepository, IDisposable
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public cSchoolCategoryRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
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


        // save category
        public async Task<bool> Save()
        {
            return await Task.Run(async () =>
            {
                return (await _context.SaveChangesAsync() >= 0);
            });

        }


        //Get category by Id
        public async Task<SchoolCategoryDto> ListById(Guid id)
        {
            return await Task.Run(async () =>
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var result = await _context.SchoolCategories.Include(x => x.Schools).Include(x => x.Centres).FirstOrDefaultAsync(c => c.Id == id);
                //return the mapped object
                return _mapper.Map<SchoolCategoryDto>(result);
            });
        }

        //Get all Category and associated entities
        public async Task<IEnumerable<SchoolCategoryDto>> List()
        {
            return await Task.Run(async () =>
            {
                var result = await _context.SchoolCategories.Include(s => s.Schools).Include(s => s.Centres).ToListAsync();
                return _mapper.Map<IEnumerable<SchoolCategoryDto>>(result);

            });
        }


        //Create Section



        public async Task<SchoolCategoryDto> Create(CreateSchoolCategoryDto categories)
        {
            return await Task.Run(async () =>
            {
                if (categories == null)
                {
                    throw new ArgumentNullException(nameof(categories));
                }
                var categoryEntity = _mapper.Map<SchoolCategories>(categories);
                categoryEntity.Id = Guid.NewGuid();

                _context.SchoolCategories.Add(categoryEntity);

                //call the save method
                bool saveResult = await Save();

                return _mapper.Map<SchoolCategoryDto>(categoryEntity);
            });
        }

        public async Task<SchoolCategoryDto> Update(Guid id, UpdateSchoolCategoryDto categories)
        {
            return await Task.Run(async () =>
            {
                if (categories == null)
                {
                    throw new ArgumentNullException(nameof(categories));
                }

                var user = await _context.SchoolCategories.FirstOrDefaultAsync(c => c.Id == id);

                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }
                var val = _mapper.Map(categories, user);

                //val.Name = categories.Name;
                //val.Code = categories.Code;
                _context.SchoolCategories.Update(val);
                bool save = await Save();

                return _mapper.Map<SchoolCategoryDto>(val);
            });
        }


        public async Task<SchoolCategoryDto> Delete(Guid catId)
        {
            return await Task.Run(async () =>
            {
                var user = await _context.SchoolCategories.FirstOrDefaultAsync(c => c.Id == catId);
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(catId));
                }
                // await _context.SaveChangesAsync();
                _context.SchoolCategories.Remove(user);
                await Save();

                return _mapper.Map<SchoolCategoryDto>(user);
            });
        }

        public async Task<bool> SchoolCategoriesExists(Guid catId)
        {
            return await Task.Run(async () =>
            {
                if (catId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(catId));
                }

                return await _context.SchoolCategories.AnyAsync(a => a.Id == catId);
            });
        }

        public async Task<bool> SchoolExists(Guid schoolId)
        {
            return await Task.Run(async () =>
            {
                if (schoolId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(schoolId));
                }

                return await _context.Schools.AnyAsync(a => a.Id == schoolId);
            });
        }

        public async Task<SchoolCategoryDto> Patch(Guid userId, JsonPatchDocument<UpdateSchoolCategoryDto> patchDocument)
        {
            return await Task.Run(async () =>
            {
                if (patchDocument == null)
                {
                    throw new ArgumentNullException(nameof(patchDocument));
                }

                var user = await _context.SchoolCategories.FirstOrDefaultAsync(c => c.Id == userId);

                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                //map the extracted result with the Update class
                var userToPatch = _mapper.Map<UpdateSchoolCategoryDto>(user);

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

                return _mapper.Map<SchoolCategoryDto>(user);
            });
        }

        public override ActionResult ValidationProblem(
            [ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices
                .GetRequiredService<IOptions<ApiBehaviorOptions>>();

            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);

        }








        public Task<CustomSchoolPageList> GetAllSchoolsForACategoryAsPagedListAsync(Guid id, SchoolsResourceParams resourceParams)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SchoolCategoriesExists(Guid catId, string name)
        {
            throw new NotImplementedException();
        }

        //return paged list
        public async Task<PagedList<SchoolCategoryDto>> List(SchoolCategoriesResourceParams resourceParams)
        {
            return await Task.Run(async () =>
            {
                if (resourceParams == null)
                {
                    throw new ArgumentNullException(nameof(resourceParams));
                }


                var collection = _context.SchoolCategories.Include(x => x.Centres)
                    .Include(x => x.Schools) as IQueryable<SchoolCategories>;



                //Search by
                if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                {
                    var searchQuery = resourceParams.SearchQuery.Trim();
                    collection = collection.Where(c => c.Name.Contains(searchQuery)

                   || c.Code.Contains(searchQuery));
                }

                //Order by
                if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                {
                    //get property mapping
                    var usersPropertyMappingDictionary =
                        _propertyMappingService.GetPropertyMapping<SchoolCategoryDto, SchoolCategories>();

                    collection = collection.ApplySort(resourceParams.OrderBy,
                        usersPropertyMappingDictionary);
                }

                //Get the mapped data
                var mappingData = (_mapper.Map<IEnumerable<SchoolCategoryDto>>(collection)).AsQueryable();// as IQueryable<AccountsDto>;

                //return the paginated data
                return  PagedList<SchoolCategoryDto>.Create(mappingData, resourceParams.PageNumber, resourceParams.PageSize);
            });
        }

        //gets a list of schools from a category
        public async Task<IEnumerable<SchoolsViewDto>> ListSchoolsByCategory(Guid catID)
        {
            return await Task.Run(async () => {

                if (catID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(catID));
                }

                var result = await _context.Schools.Where(s => s.Category.Id == catID).Select(a => a.Category).ToListAsync();

                return _mapper.Map<IEnumerable<SchoolsViewDto>>(result);

            });
        }

        //gets a list of centres from a category
        public async Task<IEnumerable<SchoolCategoryDto>> ListCentresForACategory(Guid catID)
        {
            return await Task.Run(async () => {

                if (catID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(catID));
                }

                var result = await _context.Centres.Where(s => s.SchoolCategory.Id == catID).ToListAsync();

                return _mapper.Map<IEnumerable<SchoolCategoryDto>>(result);

            });
        }

        //Gets a category detail from a center 
        public async Task<SchoolCategoryDto> ListCentresForACategoryById(Guid catID)
        {
            return await Task.Run(async () => {

                if (catID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(catID));
                }

                var result = await _context.Centres.Where(c => c.Id == catID).Select(a => a.SchoolCategory).FirstOrDefaultAsync();

                return _mapper.Map<SchoolCategoryDto>(result);

            });
        }


        public async Task<SchoolCategoryDto> ListCategoryForACentreById(Guid centID)
        {
            return await Task.Run(async () => {

                if (centID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(centID));
                }

                var result = await _context.SchoolCategories.Where(c => c.Id == centID).Select(a => a.Centres).FirstOrDefaultAsync();

                return _mapper.Map<SchoolCategoryDto>(result);

            });
        }

        public async Task<SchoolCategoryDto> ListCategoryForASchoolById(Guid schID)
        {
            return await Task.Run(async () => {

                if (schID == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(schID));
                }

                var result = await _context.Schools.Where(c => c.Id == schID).Select(a => a.Category).FirstOrDefaultAsync();

                return _mapper.Map<SchoolCategoryDto>(result);

            });
        }

        Task<CentresViewDto> ISchoolCategoryRepository.ListCentresForACategoryById(Guid catID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SchoolCategoryDto>> ListSchoolsForACategory(Guid catID)
        {
            throw new NotImplementedException();
        }

        public Task<SchoolCategoryDto> GetByCode(string code)
        {
            throw new NotImplementedException();
        }
    }
}
