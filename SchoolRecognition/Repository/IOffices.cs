using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Repository
{
    public interface IOffices
    {
      //   object Offices { get; set; }

        Task<int> Delete(Guid officesId);
        Task<Offices> GetByOfficesid(Guid officesId);
        Task<int> Update(Offices offices);
        Task<int> Create(Offices offices);
        Task<List<Offices>> ListAll();
    }
}
