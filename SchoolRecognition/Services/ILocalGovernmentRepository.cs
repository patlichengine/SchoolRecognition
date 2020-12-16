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
    public interface ILocalGovernmentRepository
    {


        //schools interface calls

        public Task<LocalGovernmentsDto> ListById(Guid id);

       // public Task<IEnumerable<LocalGovernmentsDto>> List();

        public Task<LocalGovernmentsDto> Create(LocalGovernmentsViewDto localGovernment);

      

        public Task<LocalGovernmentsDto> Update(Guid id, UpdateLocalGovernmentsDto localGovernment);
        public Task<LocalGovernmentsDto> Patch(Guid userId, JsonPatchDocument<UpdateLocalGovernmentsDto> patchDocument);
        public Task<LocalGovernmentsDto> Delete(Guid localGovernmentId);

        public Task<bool> LocalGovtExists(Guid localGovernmentId);
       
        public Task<bool> Save();
        public Task<IEnumerable<StatesViewDto>> GetAllStates();
       public Task<PagedList<LocalGovernmentsDto>> List(LocalGovernmentsResourceParams resourceParams);

        //public void Dispose();


    }

}
