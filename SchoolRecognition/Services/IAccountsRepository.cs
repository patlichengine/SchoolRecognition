using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface ISchoolCategoryRepository
    {
        public Task<bool> AccountExists();
        public Task<bool> AccountExists(Guid userId);
        public void CreateAccount(RegisterViewModel model);
        public Task<AccountsDto> GetAccount(Guid id);

        public Task<AccountsDto> GetAccount(string emailAddress, string password);

        public Task<IEnumerable<AccountsDto>> GetAccounts();
        public Task<IEnumerable<AccountsDto>> GetRoleAccounts(Guid roleId);

    }
}
