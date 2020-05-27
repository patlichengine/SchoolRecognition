using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface ISubjectsRepository
    {

        Task<IEnumerable<SubjectsViewDto>> List();
        Task<PagedList<SubjectsViewDto>> PagedList(SubjectsResourceParams resourceParams);
        Task<SubjectsViewDto> Get(Guid id);
        Task<Guid?> Create(SubjectsCreateDto _obj);
        Task<SubjectsViewDto> Update(SubjectsCreateDto _obj);
        Task Delete(Guid id); //return type is void
        ///
        Task<bool> Exists(string subjectCode, string longName, string shortName);
        Task<bool> Exists(Guid id, string subjectCode, string longName, string shortName);
    }
}
