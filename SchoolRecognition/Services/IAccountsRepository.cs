using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IAccountsRepository
    {
         Task<bool> AccountExists();
         Task<bool> AccountExists(Guid userId);
         void CreateAccount(RegisterViewModel model);
         Task<AccountsDto> GetAccount(Guid id);

         Task<AccountsDto> GetAccount(string emailAddress, string password);

         Task<IEnumerable<AccountsDto>> GetAccounts();
         Task<IEnumerable<AccountsDto>> GetRoleAccounts(Guid roleId);

    }
}
