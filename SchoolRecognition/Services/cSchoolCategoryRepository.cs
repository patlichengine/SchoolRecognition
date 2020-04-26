
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SchoolRecognition.Services
{
    public class cSchoolCategoryRepository : ISchoolCategoryRepository, IDisposable
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;

        public cSchoolCategoryRepository(SchoolRecognitionContext  context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
        public async Task<SchoolCategoryDto> GetCategoryById(Guid id)
        {
            return await Task.Run(async () =>
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var result = await _context.SchoolCategories.FirstOrDefaultAsync(c => c.Id == id);
                //return the mapped object
                return _mapper.Map<SchoolCategoryDto>(result);
            });
        }

        //Get all Category
        public async Task<IEnumerable<SchoolCategoryDto>> GetAllCategory()
        {
            return await Task.Run(async () =>
            {
                var result = await _context.SchoolCategories.ToListAsync<SchoolCategories>();
                return _mapper.Map<IEnumerable<SchoolCategoryDto>>(result);

            });
        }


        //Delete Section

       

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
                categoryEntity.Name = categories.Name;
                categoryEntity.Code = categories.Code;
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
                var val =   _mapper.Map(user, user);

                val.Name = categories.Name;
                val.Code = categories.Code;
                _context.SchoolCategories.Update(val);
                bool save = await Save();

                return _mapper.Map<SchoolCategoryDto>(val);
            });
        }


        public async Task<SchoolCategoryDto> DeleteSchoolCategory(Guid catId)
        {
            return await Task.Run(async () =>
            {
                var user = await _context.SchoolCategories.FirstOrDefaultAsync(c => c.Id == catId);
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(catId));
                }

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

        public Task<IEnumerable<SchoolsDto>> GetAllSchoolsForACategory()
        {
            throw new NotImplementedException();
        }
    }
}
