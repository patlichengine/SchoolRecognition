using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IUserRepository
    {
        Task<UserDto>  Authenticate(string username, byte[] password);
        //IEnumerable<UserDto> GetAll();
        Task<UserDto> GetById(Guid id);
        //UserDto Create(UserDto user, string password);
        //void Update(UserDto user, string password = null);
        //void Delete(int id);
    }
   
}
