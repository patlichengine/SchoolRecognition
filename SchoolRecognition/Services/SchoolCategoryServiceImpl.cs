using SchoolRecognition.Models;
using SchoolRecognition.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public class SchoolCategoryServiceImpl : SchoolCategoriesRepo
    {
        private readonly SchoolCategoriesService _schoolCategoriesService;
        public SchoolCategoryServiceImpl(SchoolCategoriesService schoolCategoriesService)
        {
            _schoolCategoriesService = schoolCategoriesService;

        }

        public bool Create(SchoolCategories schoolCategories)
        {
            return _schoolCategoriesService.Create(schoolCategories);
        }

        public SchoolCategories GetBySchoolCategoriesId(Guid schoolCategoriesId)
        {
            return _schoolCategoriesService.GetBySchoolCategoriesId(schoolCategoriesId);
        }

        public IList<SchoolCategories> ListAll()
        {
            return _schoolCategoriesService.ListAll();
        }

        public bool Update(SchoolCategories schoolCategories)
        {
            return _schoolCategoriesService.Update(schoolCategories);
        }

        public bool Delete(Guid schoolCategoriesId)
        {
            return _schoolCategoriesService.Delete(schoolCategoriesId);
        }
    }

}