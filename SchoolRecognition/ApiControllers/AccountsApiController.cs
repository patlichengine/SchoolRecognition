using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRecognition.Models;
using SchoolRecognition.Services;

namespace SchoolRecognition.ApiControllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsApiController : ControllerBase
    {
        private readonly ISchoolCategoryRepository _accountsRepository;

        public AccountsApiController(ISchoolCategoryRepository accountsRepository)
        {
            _accountsRepository = accountsRepository ??
               throw new ArgumentNullException(nameof(accountsRepository));
        }
        // GET: api/accounts
        [HttpGet()]
        public ActionResult<IEnumerable<AccountsDto>> Get()
        {
            if (!_accountsRepository.AccountExists().Result)
            {
                return NotFound();
            }
            var result = _accountsRepository.GetAccounts().Result;
            return Ok(result);
        }

        // GET: api/accounts/5
        [HttpGet()]
        [Route("{userId}")]
        public IActionResult Get(Guid userId)
        {
            if(userId == Guid.Empty)
            {
                return NotFound();
            }

            var result = _accountsRepository.GetAccount(userId).Result;
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("{emailAddress}/{password}")]
        public IActionResult Get(string emailAddress, string password)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentNullException(nameof(emailAddress));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            var result = _accountsRepository.GetAccount(emailAddress, password).Result;
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }



        // POST: api/accounts
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/accounts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/accounts/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
