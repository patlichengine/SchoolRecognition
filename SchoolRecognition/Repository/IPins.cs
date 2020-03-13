using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Repository
{
    public interface IPins
    {
        Task<List<Pins>> Get();
        Task<Pins> Get(Guid id);
        Task<Guid?> Create(Pins _obj);
        Task<Pins> Update(Pins _obj);
        Task Delete(Guid pinId); //return type is void
    }
}
