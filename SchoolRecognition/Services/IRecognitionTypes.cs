using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IRecognitionTypes
    {

        Task<List<RecognitionTypes>> Get();
        Task<RecognitionTypes> Get(Guid id);
        Task<Guid?> Create(RecognitionTypes _obj);
        Task<RecognitionTypes> Update(RecognitionTypes _obj);
        Task Delete(Guid id); //return type is void
    }
}
