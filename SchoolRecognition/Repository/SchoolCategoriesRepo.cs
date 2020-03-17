using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Repository
{
    public interface SchoolCategoriesRepo
    {

        Task<int> Delete(Guid schoolCategoriesId);
        Task<SchoolCategories> GetBySchoolCategoriesId(Guid schoolCategoriesId);
        Task<int> Update(SchoolCategories schoolCategories);
       public Task<int?> Create(SchoolCategories schoolCategories);
        public Task<List<SchoolCategories>> ListAll();
    }
}
