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
    public interface ILocationRepository
    {


        //schools interface calls

        public Task<LocationDto> ListById(Guid id);

        public Task<PagedList<LocationDto>> List(LocationTypesResourceParams resourceParams);

        public Task<LocationDto> Create(CreateLocationDto locations);

      

        public Task<LocationDto> Update(Guid id, UpdateLocationDto updateLocationDto);
        public Task<LocationDto> Patch(Guid userId, JsonPatchDocument<UpdateLocationDto> patchDocument);
        public Task<LocationDto> Delete(Guid locationId);

        public Task<bool> LocationsExists(Guid locationId);
       
        public Task<bool> Save();


       

    }

}
