using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Repository
{
    interface IOfficeStates
    {
        //   object Offices { get; set; }

        Task<int> Delete(Guid StatesId);
        Task<States> GetByStatesid(Guid statesId);
        Task<int> Update(States states);
        Task<int> Create(States states);
        Task<List<States>> ListAll();
    }
}
