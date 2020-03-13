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
        Task<Pins> Get(Guid pinId);
        Task<Pins> Create(Pins _obj);
        Task<Pins> Update(Pins _obj);
        Task<Pins> Delete(Guid pinId);
    }
}
