using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Repository
{
    public interface SchoolCategoriesRepo
    {

        bool Delete(Guid schoolCategoriesId);
        SchoolCategories GetBySchoolCategoriesId(Guid schoolCategoriesId);
        bool Update(SchoolCategories schoolCategories);
        bool Create(SchoolCategories schoolCategories);
        public IList<SchoolCategories> ListAll();
    }
}
