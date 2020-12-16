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
    public interface IFacilityItemsRepository
    {
      
          public  Task<IEnumerable<FacilityItemsDto>> List();
          public  Task<PagedList<FacilityItemsDto>> List(FacilityItemsResourceParams itemsResourceParams);
          public  Task<FacilityItemsDto> Create(FacilityItemsCreateDto create);
          public  Task<FacilityItemsDto> Update(Guid facSettingsID, FacilityItemsUpdateDto update);
          public  Task<FacilityItemsDto> ListById(Guid listByID);
        public Task<FacilityItemsDto> Patch(Guid facSettingsID, JsonPatchDocument<FacilityItemsUpdateDto> patchDocument);
            public Task<FacilityItemsDto> Delete(Guid delete);

     
        public Task<IEnumerable<SubjectsViewDto>> GetAllSubjects();
        public Task<bool> FacilityItemsExists(Guid ItemsID);

        public Task<IEnumerable<FacilityTypesDto>> GetAllFacilityTypes();

    }
}
