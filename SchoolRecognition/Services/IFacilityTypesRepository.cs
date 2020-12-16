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
    public interface IFacilityTypesRepository
    {
      
          public  Task<PagedList<FacilityTypesDto>> List(FacilityTypesResourceParams resourceParams);
          public  Task<FacilityTypesDto> Create(CreateFacilityTypesDto createFacility);
          public  Task<FacilityTypesDto> Update(Guid facTypesID, UpdateFacilityTypesDto updateFacility);
          public  Task<FacilityTypesDto> ListById(Guid facTypesID);
        public Task<FacilityTypesDto> Patch(Guid facTypesID, JsonPatchDocument<UpdateFacilityTypesDto> patchDocument);
            public Task<FacilityTypesDto> Delete(Guid facTypesID);

            public Task<bool> FacilityTypesExists(Guid facTypesID);
            public Task<bool> Save();
        
    }
}
