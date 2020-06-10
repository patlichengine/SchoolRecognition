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
    public interface ISchoolsRepository
    {

        Task<IEnumerable<SchoolsViewDto>> List();
        Task<PagedList<SchoolsViewDto>> PagedList(SchoolsResourceParams resourceParams);
        Task<SchoolsViewDto> Get(Guid id);
        Task<SchoolsViewDto> GetIncludingListOfSchoolPayments(Guid id);
        Task<SchoolsViewDto> GetBySchoolName(string schoolName);
        Task<SchoolsViewPagedListSchoolFacilitiesDto> GetIncludingPagedListOfSchoolFacilities(Guid id, SchoolFacilitiesResourceParams resourceParams);
        Task<Guid?> Create(SchoolsCreateDto _obj);
        Task<Guid?> CreateForCentre(CentresViewDto _obj, Guid? officeId);
        Task<SchoolsViewDto> Update(SchoolsCreateDto _obj);
        Task Delete(Guid id); //return type is void
        ///
        Task<bool> Exists(string schoolName);
        Task<bool> Exists(Guid id, string schoolName);
        Task<bool> Exists(string schoolName, string schoolAddress);
        Task<bool> Exists(Guid id, string schoolName, string schoolAddress);
        Task<SchoolsCreationDependecyDto> GetCreationDependencys();
    }

}
