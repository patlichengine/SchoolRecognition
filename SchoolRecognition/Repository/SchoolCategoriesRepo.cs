using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Repository
{
    public interface SchoolCategoriesRepo
    {

        public Task<List<SchoolCategories>> List();

        public Task<int> Delete(Guid id);

        public Task<int> Create(SchoolCategories categories);

        public Task<int> Update(SchoolCategories categories);

        public Task<SchoolCategories> GetById(Guid id);

    }
}
