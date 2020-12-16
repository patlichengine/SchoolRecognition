using Microsoft.AspNetCore.JsonPatch;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface ISchoolClassesRepository
    {

        public Task<IEnumerable<SchoolClassesDto>> List();
        public Task<IEnumerable<SchoolClassesDto>> GetAllSchoolClassesAllocations(Guid id);
        public Task<IEnumerable<SchoolClassesDto>> GetAllSchoolClassesSchoolSubjectStaffs(Guid id);
        Task<PagedList<SchoolClassesDto>> List(SchoolClassesResourceParams resourceParams);
        Task<PagedList<SchoolClassesDto>> GetAllSchoolClassesAllocationsAsPagedList(Guid id, SchoolClassesAllocationsResourceParams resourceParams);
        Task<PagedList<SchoolClassesDto>> GetAllSchoolClassesSchSubStaffsAsPagedList(Guid id, SchoolClassesSchSubStaffsResourceParams resourceParams);

        public Task<SchoolClassesDto> Create(CreateSchoolClassesDto createSchool);
        public Task<SchoolClassesDto> Update(Guid schoolClassID, UpdateSchoolClassesDto updateSchool);
        public Task<SchoolClassesDto> ListById(Guid id);
        public Task<SchoolClassesDto> Patch(Guid schoolClassID, JsonPatchDocument<UpdateSchoolClassesDto> patchDocument);
        public Task<SchoolClassesDto> Delete(Guid schoolClassID);

        public Task<bool> SchoolClassesExists(Guid schoolClassID);
        public Task<bool> SchoolClassesExists(string schoolClassID);
        public Task<bool> Save();
        public Task<SchoolClassesDependencyDto> GetSchoolClassCreateDepedencys();

    }
}
