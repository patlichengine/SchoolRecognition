
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SchoolRecognition.Services
{
    public class cSchoolsRepository : ISchoolsRepository, IDisposable
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;

        public cSchoolsRepository(SchoolRecognitionContext  context, IMapper mapper)
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
        public async Task<SchoolsDto> GetSchoolsById(Guid id)
        {
            return await Task.Run(async () =>
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var result = await _context.Schools.FirstOrDefaultAsync(c => c.Id == id);
                //return the mapped object
                return _mapper.Map<SchoolsDto>(result);
            });
        }

        //Get all Category
        public async Task<IEnumerable<SchoolsDto>> GetAllSchools()
        {
            return await Task.Run(async () =>
            {
                var result = await _context.Schools.ToListAsync();
                return _mapper.Map<IEnumerable<SchoolsDto>>(result);

            });
        }


        //Delete Section
        public async Task<SchoolsDto> Create(CreateSchoolsDto categories)
        {
            return await Task.Run(async () =>
            {
                if (categories == null)
                {
                    throw new ArgumentNullException(nameof(categories));
                }
                var categoryEntity = _mapper.Map<Schools>(categories);
                categoryEntity.Id = Guid.NewGuid();
                categoryEntity.Name = categories.Name;
                categoryEntity.Address = categories.Address;
                categoryEntity.EmailAddress = categories.EmailAddress;
                categoryEntity.PhoneNo = categories.PhoneNo;
                categoryEntity.LgId = categories.LgId;
                categoryEntity.OfficeId = categories.OfficeId;
                categoryEntity.CategoryId = categories.CategoryId;
                _context.Schools.Add(categoryEntity);

                //call the save method
                bool saveResult = await Save();

                return _mapper.Map<SchoolsDto>(categoryEntity);
            });
        }

        public async Task<SchoolsDto> Update(Guid id, UpdateSchoolsDto categories)
        {
            return await Task.Run(async () =>
            {
                if (categories == null)
                {
                    throw new ArgumentNullException(nameof(categories));
                }

                var user = await _context.Schools.FirstOrDefaultAsync(c => c.Id == id);

                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }
                var categoryEntity =   _mapper.Map(user, user);
                categoryEntity.Name = categories.Name;
                categoryEntity.Address = categories.Address;
                categoryEntity.EmailAddress = categories.EmailAddress;
                categoryEntity.PhoneNo = categories.PhoneNo;
                categoryEntity.LgId = categories.LgId;
                categoryEntity.OfficeId = categories.OfficeId;
                categoryEntity.CategoryId = categories.CategoryId;
               
                _context.Schools.Update(categoryEntity);
                bool save = await Save();

                return _mapper.Map<SchoolsDto>(categoryEntity);
            });
        }


        

        public async Task<bool> SchoolsExists(Guid catId)
        {
            return await Task.Run(async () =>
            {
                if (catId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(catId));
                }

                return await _context.Schools.AnyAsync(a => a.Id == catId);
            });
        }

        public async Task<SchoolsDto> DeleteSchools(Guid schoolId)
        {
            return await Task.Run(async () =>
            {
                var user = await _context.Schools.FirstOrDefaultAsync(c => c.Id == schoolId);
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(schoolId));
                }

                _context.Schools.Remove(user);
                await Save();

                return _mapper.Map<SchoolsDto>(user);
            });
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

        public async Task<OfficesDto> GetOfficesById(Guid id)
        {
            return await Task.Run(async () =>
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var result = await _context.Offices.FirstOrDefaultAsync(c => c.Id == id);
                //return the mapped object
                return _mapper.Map<OfficesDto>(result);
            });
        }

        public async Task<IEnumerable<OfficesDto>> GetAllOffices()
        {
            return await Task.Run(async () =>
            {
                var result = await _context.Offices.ToListAsync();
                return _mapper.Map<IEnumerable<OfficesDto>>(result);

            });
        }

        public async Task<LocalGovernmentsDto> GetLocalGovernmentsById(Guid id)
        {
            return await Task.Run(async () =>
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var result = await _context.LocalGovernments.FirstOrDefaultAsync(c => c.Id == id);
                //return the mapped object
                return _mapper.Map<LocalGovernmentsDto>(result);
            });
        }

        public async Task<IEnumerable<LocalGovernmentsDto>> GetAllLocalGovernments()
        {
            return await Task.Run(async () =>
            {
                var result = await _context.LocalGovernments.ToListAsync();
                return _mapper.Map<IEnumerable<LocalGovernmentsDto>>(result);

            });
        }
    }
}
