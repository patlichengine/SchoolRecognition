using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IAccountsRepository
    {
        Task<bool> AccountExists();
        Task<bool> AccountExists(Guid userId);
        Task<AccountsDto> CreateAccount(AccountsCreateDto user);
        Task<AccountsDto> GetAccount(Guid id);
        Task<bool> GetAccountByMail(string email);
        Task<AccountsDto> GetAccount(string emailAddress, string password);
        Task<IEnumerable<AccountsDto>> GetAccounts();
        Task<IEnumerable<AccountsDto>> GetRoleAccounts(Guid roleId);
        Task<PagedList<AccountsDto>> GetAccounts(UserResourceParameters usersResourceParameters);
        Task<bool> Save();
    }
}