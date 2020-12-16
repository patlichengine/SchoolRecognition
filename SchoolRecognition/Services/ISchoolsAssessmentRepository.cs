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
    public interface ISchoolsAssessmentRepository
    {
      
          public  Task<IEnumerable<SchoolsAssessmentDto>> List();
          public  Task<PagedList<SchoolsAssessmentDto>> List(SchoolsAssessmentResourceParams itemsResourceParams);
          public  Task<SchoolsAssessmentDto> Create(SchoolsAssessmentCreateDto create);
          public  Task<SchoolsAssessmentDto> Update(Guid facSettingsID, SchoolsAssessmentUpdateDto update);
          public  Task<SchoolsAssessmentDto> ListById(Guid listByID);
        public Task<SchoolsAssessmentDto> Patch(Guid facSettingsID, JsonPatchDocument<SchoolsAssessmentUpdateDto> patchDocument);
            public Task<SchoolsAssessmentDto> Delete(Guid delete);

     
        public Task<IEnumerable<SubjectsViewDto>> GetAllSubjects();
        public Task<bool> FacilityItemsExists(Guid ItemsID);

        public Task<IEnumerable<FacilityTypesDto>> GetAllFacilityTypes();

    }
}
