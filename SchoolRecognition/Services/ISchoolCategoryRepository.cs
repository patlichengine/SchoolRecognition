using Microsoft.AspNetCore.JsonPatch;
using SchoolRecognition.Entities;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface ISchoolCategoryRepository
    {




        public Task<SchoolCategoryDto> ListById(Guid id);

        public Task<IEnumerable<SchoolCategoryDto>> List();
        public Task<PagedList<SchoolCategoryDto>> List(SchoolCategoriesResourceParams resourceParams);

        //returns a list of centres for a categories 
        public Task<IEnumerable<SchoolCategoryDto>> ListSchoolsForACategory(Guid catID);

        //returns a list of centres for a schoolcategory
        public Task<IEnumerable<SchoolCategoryDto>> ListCentresForACategory(Guid catID);


        //returns a list of centres of a category
        public Task<CentresViewDto> ListCentresForACategoryById(Guid catID);

        //List a category detail by id
        public Task<SchoolCategoryDto> ListCategoryForACentreById(Guid centID);

        //List a category detail for a school
        public Task<SchoolCategoryDto> ListCategoryForASchoolById(Guid schID);

        //public Task<SchoolCategoryDto> GetOfficesAllSchoolsAsync(Guid id);
        Task<CustomSchoolPageList> GetAllSchoolsForACategoryAsPagedListAsync(Guid id, SchoolsResourceParams resourceParams);

        Task<SchoolCategoryDto> GetByCode(string code);

        public Task<SchoolCategoryDto> Create(CreateSchoolCategoryDto categories);

        public Task<SchoolCategoryDto> Update(Guid id, UpdateSchoolCategoryDto categories);

        public Task<SchoolCategoryDto> Patch(Guid userId, JsonPatchDocument<UpdateSchoolCategoryDto> patchDocument);

        public Task<SchoolCategoryDto> Delete(Guid catId);

        public Task<bool> SchoolCategoriesExists(Guid catId);
        public Task<bool> SchoolCategoriesExists(Guid catId, string name);
        public Task<bool> SchoolExists(Guid schoolId);
        public Task<bool> Save();
    }

}
