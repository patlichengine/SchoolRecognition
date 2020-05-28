using Microsoft.AspNetCore.JsonPatch;
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




        public Task<SchoolCategoryDto> GetCategoryById(Guid id);

        public Task<IEnumerable<SchoolCategoryDto>> List();
        public Task<IEnumerable<SchoolsViewDto>> GetAllSchoolsForACategory();


        public Task<SchoolCategoryDto> Create(CreateSchoolCategoryDto categories);

        public Task<SchoolCategoryDto> Update(Guid id, UpdateSchoolCategoryDto categories);


        public Task<SchoolCategoryDto> DeleteSchoolCategory(Guid catId);

        public Task<bool> SchoolCategoriesExists(Guid catId);
        public Task<bool> Save();
    }

}
