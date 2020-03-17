using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Repository
{
    public interface IAccount
    {
        public Task<int> AssignRole(Guid user_id, Guid role_id);
        public Task<int> CreateUser(RegisterViewModel model);
        public Task<Users> Get(Guid id);

        public Task<List<Users>> List();
        public Task<List<Users>> List(Guid userGroupID);

    }
}
