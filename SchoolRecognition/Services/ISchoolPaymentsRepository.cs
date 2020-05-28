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
    public interface ISchoolPaymentsRepository
    {

        Task<IEnumerable<SchoolPaymentsViewDto>> List();
        Task<PagedList<SchoolPaymentsViewDto>> PagedList(SchoolPaymentsResourceParams resourceParams);
        Task<SchoolPaymentsViewDto> Get(Guid id);
        Task<Guid?> Create(SchoolPaymentsCreateDto _obj);
        Task<SchoolPaymentsViewDto> Update(SchoolPaymentsCreateDto _obj);
        Task Delete(Guid id); //return type is void
        ///
        Task<bool> Exists(string receiptNo);
        Task<bool> Exists(Guid id, string receiptNo);

    }
}
