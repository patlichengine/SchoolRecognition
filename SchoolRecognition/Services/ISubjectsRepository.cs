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

        Task<IEnumerable<SubjectsViewDto>> GetAllSubjectsAsync();
        Task<CustomPagedList<SubjectsViewDto>> GetAllSubjectsAsPagedListAsync(SubjectsResourceParams resourceParams);
        Task<SubjectsViewDto> GetSubjectsSingleOrDefaultAsync(Guid id);
        Task<Guid?> CreateSubjectAsync(SubjectsCreateDto _obj);
        Task<SubjectsViewDto> UpdateSubjectAsync(SubjectsCreateDto _obj);
        Task DeleteSubjectAsync(Guid id); //return type is void
    }
}
