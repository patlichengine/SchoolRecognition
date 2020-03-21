using SchoolRecognition.Classes;
using SchoolRecognition.Models;
using SchoolRecognition.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services 
{
    public class OfficesRepoImpl : IOffices
    {
        private readonly clsOffices _offices;

        public OfficesRepoImpl(clsOffices offices)
        {
            _offices = offices;
        }
        public Task<int> Create(Offices offices)
        {
            return _offices.Create(offices);
        }

        public Task<int> Delete(Guid officesId)
        {
            return _offices.Delete(officesId);
        }

        public Task<Offices> GetByOfficesid(Guid officesId)
        {
            return _offices.GetByOfficesid(officesId);
        }

        public Task<List<Offices>> ListAll()
        {
            return _offices.ListAll();
        }

        public Task<int> Update(Offices offices)
        {
            return _offices.Update(offices);
        }
    }
}
