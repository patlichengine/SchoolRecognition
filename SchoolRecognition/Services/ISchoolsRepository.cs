using Microsoft.AspNetCore.JsonPatch;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface ISchoolsRepository
    {


        //schools interface calls

        public Task<SchoolsDto> GetSchoolsById(Guid id);

        public Task<IEnumerable<SchoolsDto>> GetAllSchools();


        public Task<SchoolsDto> Create(CreateSchoolsDto createSchoolsDto);

        public Task<SchoolsDto> Update(Guid id, UpdateSchoolsDto updateSchoolsDto);

        public Task<SchoolsDto> DeleteSchools(Guid schoolId);

        public Task<bool> SchoolsExists(Guid schoolId);
        public Task<bool> Save();


        //get category
        public Task<SchoolCategoryDto> GetCategoryById(Guid id);

        public Task<IEnumerable<SchoolCategoryDto>> GetAllCategory();

        //get offices
        public Task<OfficesViewDto> GetOfficesById(Guid id);

        public Task<IEnumerable<OfficesViewDto>> GetAllOffices();

        //get lga

        public Task<LocalGovernmentsDto> GetLocalGovernmentsById(Guid id);

        public Task<IEnumerable<LocalGovernmentsDto>> GetAllLocalGovernments();

    }

}
