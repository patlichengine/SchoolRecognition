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




        public Task<SchoolCategorysViewDto> GetCategoryById(Guid id);
        Task<SchoolCategorysViewDto> GetByCode(string code);
        public Task<IEnumerable<SchoolCategorysViewDto>> List();
        public Task<IEnumerable<SchoolsViewDto>> GetAllSchoolsForACategory();


        public Task<SchoolCategorysViewDto> Create(SchoolCategorysCreateDto categories);

        public Task<SchoolCategorysViewDto> Update(Guid id, UpdateSchoolCategoryDto categories);


        public Task<SchoolCategorysViewDto> DeleteSchoolCategory(Guid catId);

        public Task<bool> SchoolCategoriesExists(Guid catId);
        public Task<bool> Save();
    }

}
