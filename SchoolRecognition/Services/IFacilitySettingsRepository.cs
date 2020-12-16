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
    public interface IFacilitySettingsRepository
    {
      
          //public  Task<IEnumerable<FacilitySettingsDto>> List();
          public  Task<PagedList<FacilitySettingsDto>> List(FacilitySettingsResourceParams settingsResourceParams);
          public  Task<FacilitySettingsDto> Create(CreateFacilitySettingsDto createFacSettings);
          public  Task<FacilitySettingsDto> Update(Guid facSettingsID, UpdateFacilitySettingsDto updateFacSettings);
          public  Task<FacilitySettingsDto> ListById(Guid facSettingsID);
        public Task<FacilitySettingsDto> Patch(Guid facSettingsID, JsonPatchDocument<FacilityItemsUpdateDto> patchDocument);
            public Task<FacilitySettingsDto> Delete(Guid facSettingsID);

        public Task<FacilitySettingsDto> GetSettingsForAFacType(Guid facType, Guid facSetID);

            public Task<bool> FacilitySettingsExists(Guid facSettingsID);
            public Task<bool> Save();
        public Task<FacilitySettingsDto> CreateFacilitySecTypes(Guid FacSettingsID, CreateFacilitySettingsDto createFacility);
        public Task<IEnumerable<FacilitySettingsDto>> GetSettingsForFacType(Guid facTypeID);
        public Task<FacilitySettingsDto> GetFacilitySectTypesById(Guid FacSettingsID, Guid facTypesID);
        public Task<IEnumerable<SubjectsViewDto>> GetAllSubjects();
        public Task<bool> FacilityTypesExists(Guid facTypesID);

        public Task<IEnumerable<FacilityTypesDto>> GetAllFacilityTypes();
        Task<IEnumerable<FacilityItemsDto>> GetAllFacilityItems();
    }
}
