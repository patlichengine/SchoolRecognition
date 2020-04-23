using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface ISchoolCategoryRepository
    {


       

        public Task<SchoolCategoryDto> Delete(Guid id);

        public Task<SchoolCategoryDto> Create(SchoolCategories categories);

        public Task<SchoolCategoryDto> Update(SchoolCategoryDto categories);

        
       
        public Task<SchoolCategoryDto> GetCategoryById(Guid id);

        

        public Task<IEnumerable<SchoolCategoryDto>> GetAllCategory();
        

    }
}
