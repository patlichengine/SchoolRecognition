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
    public interface IRanksRepository
    {
        

            public Task<IEnumerable<RanksDto>> List();
            public Task<PagedList<RanksDto>> List(RanksResourceParams resourceParams);
            public Task<RanksDto> Create(CreateRanksDto ranks);
            public Task<RanksDto> Update(Guid ranksID, UpdateRanksDto ranks);
            public Task<RanksDto> ListById(Guid ranksId);
            public Task<RanksDto> Patch(Guid userId, JsonPatchDocument<UpdateRanksDto> patchDocument);
            public Task<RanksDto> Delete(Guid ranksId);

            public Task<bool> RanksExists(Guid ranksId);
            public Task<bool> Save();
       
    }
}
