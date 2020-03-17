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

        public Task<int?> Create(SchoolCategories schoolCategories)
        {
            return _schoolCategoriesService.Create(schoolCategories);
        }

        public Task<SchoolCategories> GetBySchoolCategoriesId(Guid schoolCategoriesId)
        {
            return _schoolCategoriesService.GetBySchoolCategoriesId(schoolCategoriesId);
        }

        public Task<List<SchoolCategories>> ListAll()
        {
            return _schoolCategoriesService.ListAll();
        }

        public Task<int> Update(SchoolCategories schoolCategories)
        {
            return _schoolCategoriesService.Update(schoolCategories);
        }

        public Task<int> Delete(Guid schoolCategoriesId)
        {
            return _schoolCategoriesService.Delete(schoolCategoriesId);
        }
    }

}