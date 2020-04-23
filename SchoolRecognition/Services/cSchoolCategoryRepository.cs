
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

        public Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();
        }


        public async Task<SchoolCategoryDto> Create(SchoolCategories schoolCategory)
        {
            return await Task.Run(async () =>
            {
                if (schoolCategory == null)
                {
                    throw new ArgumentNullException(nameof(schoolCategory));
                }
                var categoryEntity = _mapper.Map<SchoolCategories>(schoolCategory);
                categoryEntity.Id = Guid.NewGuid();
                //categoryEntity.Name = schoolCategory.Name;
                //categoryEntity.Code = schoolCategory.Code;

                _context.SchoolCategories.Add(categoryEntity);

                //call the save method
                bool saveResult = await Save();   //this method is also part of the Interface methods

                return _mapper.Map<SchoolCategoryDto>(categoryEntity);
            });
        }

        public async Task<bool> Save()
        {
            return await Task.Run(async () =>
            {
                return (await _context.SaveChangesAsync() >= 0);
            });

        }

        public Task<SchoolCategoryDto> Update(SchoolCategoryDto categories)
        {
            throw new NotImplementedException();
        }

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

        public async Task<IEnumerable<SchoolCategoryDto>> GetAllCategory()
        {
            return await Task.Run(async () =>
            {
                var result = await _context.SchoolCategories.ToListAsync<SchoolCategories>();
                return _mapper.Map<IEnumerable<SchoolCategoryDto>>(result);

            });
        }

       
    }
}
