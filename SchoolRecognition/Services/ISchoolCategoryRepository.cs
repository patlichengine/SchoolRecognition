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


       

        public Task<int> Delete(Guid id);

        public Task<SchoolCategoryDto> Create(SchoolCategoryDto categories);

        public Task<SchoolCategoryDto> Update(SchoolCategoryDto categories);

        
       
        public Task<SchoolCategoryDto> GetCategoryById(Guid id);

        

        public Task<IEnumerable<SchoolCategoryDto>> GetAllCategory();
        

    }
}
