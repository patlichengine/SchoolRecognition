using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IStatesRepository
    {


        Task<IEnumerable<StatesViewDto>> List();
        Task<PagedList<StatesViewDto>> PagedList(StatesResourceParams resourceParams);
        Task<StatesViewPagedListLocalGovernmentsDto> GetIncludingPagedListOfLocalGovernments(Guid id, LocalGovernmentsResourceParams resourceParams);
        Task<StatesViewDto> Get(Guid id);
        Task<StatesViewDto> GetByCode(string code);
        Task<Guid?> Create(StatesCreateDto _obj);
        Task<StatesViewDto> Update(StatesCreateDto _obj);
        Task Delete(Guid id); //return type is void
        ///

        Task<bool> Exists(string stateName);
        Task<bool> Exists(Guid id, string stateName);

    }
}
