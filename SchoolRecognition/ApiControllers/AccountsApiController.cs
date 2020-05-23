using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using SchoolRecognition.Services;

namespace SchoolRecognition.ApiControllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsApiController : ControllerBase
    {
        private readonly IAccountsRepository _accountsRepository;

        public AccountsApiController(IAccountsRepository accountsRepository)
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
        [HttpGet(Name = "GetUser")]
        [Route("{userId}")]
        public IActionResult Get(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return NotFound();
            }

            var result = _accountsRepository.GetAccount(userId).Result;
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpGet("login", Name = "GetLogin")]
        public IActionResult GetLogin([FromQuery] UserLoginParamenters loginParamenters)
        {
            if (string.IsNullOrWhiteSpace(loginParamenters.email) || string.IsNullOrWhiteSpace(loginParamenters.password))
            {
                return NotFound();
            }

            if (!_accountsRepository.AccountExists().Result)
            {
                return Ok(new AccountsDto()); //NotFound();
            }

            var objUsers = _accountsRepository.GetAccount(loginParamenters.email, loginParamenters.password).Result;
            if (objUsers == null)
            {
                return Ok(new AccountsDto()); //NotFound();
            }

            return Ok(objUsers);
        }



        // POST: api/accounts
        [HttpPost(Name = "CreateUser")]
        public ActionResult<AccountsDto> CreateUser([FromBody] AccountsCreateDto user)
        {
            var result = _accountsRepository.CreateAccount(user).Result;
            //Return the named user using the specified URI name

            //Create a links to support HATEOAS
            var links = CreateLinksForUser(result.Id, null);

            //Create a linked resource
            var linkedResourceToReturn = result.ShapeData(null)
                as IDictionary<string, object>;

            //Add the links to the linked resource
            linkedResourceToReturn.Add("links", links);

            return CreatedAtRoute("Get",
                new { userId = linkedResourceToReturn["Id"] }, linkedResourceToReturn);
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

        private string CreateUsersResourceUri(
                    UserResourceParameters usersResourceParameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetUsers",
                        new
                        {
                            fields = usersResourceParameters.Fields,
                            orderBy = usersResourceParameters.OrderBy,
                            pageNumber = usersResourceParameters.PageNumber - 1,
                            pageSize = usersResourceParameters.PageSize,
                            emailAddress = usersResourceParameters.EmailAddress,
                            searchQuery = usersResourceParameters.SearchQuery,
                        });
                case ResourceUriType.NextPage:
                case ResourceUriType.CurrentPage:
                    return Url.Link("GetUsers",
                        new
                        {
                            fields = usersResourceParameters.Fields,
                            orderBy = usersResourceParameters.OrderBy,
                            pageNumber = usersResourceParameters.PageNumber,
                            pageSize = usersResourceParameters.PageSize,
                            emailAddress = usersResourceParameters.EmailAddress,
                            searchQuery = usersResourceParameters.SearchQuery,
                        });
                default:
                    return Url.Link("GetUsers",
                        new
                        {
                            fields = usersResourceParameters.Fields,
                            orderBy = usersResourceParameters.OrderBy,
                            pageNumber = usersResourceParameters.PageNumber,
                            pageSize = usersResourceParameters.PageSize,
                            emailAddress = usersResourceParameters.EmailAddress,
                            searchQuery = usersResourceParameters.SearchQuery,
                        });
            }
        }

        private IEnumerable<LinkDto> CreateLinksForUser(Guid userId, string fields)
        {
            var links = new List<LinkDto>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(
                    new LinkDto(Url.Link("GetUser", new { userId, fields }),
                    "self",
                    "GET"));
            }
            else
            {
                links.Add(
                    new LinkDto(Url.Link("GetUser", new { userId, fields }),
                    "self",
                    "GET"));
            }

            links.Add(
                    new LinkDto(Url.Link("DeleteUser", new { userId }),
                    "delete_user",
                    "DELETE"));

            links.Add(
                    new LinkDto(Url.Link("CreateUserAudit", new { userId }),
                    "create_user_audit_trail",
                    "POST"));

            //GetUserAuditTrail
            links.Add(
                    new LinkDto(Url.Link("GetUserAuditTrails", new { userId }),
                    "audit_trail",
                    "GET"));

            return links;
        }

        public IEnumerable<LinkDto> CreateLinksForUsers(
            UserResourceParameters usersResourceParameters,
            bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            //self
            links.Add(
                new LinkDto(CreateUsersResourceUri(
                usersResourceParameters, ResourceUriType.CurrentPage)
                , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                new LinkDto(CreateUsersResourceUri(
                usersResourceParameters, ResourceUriType.NextPage)
                , "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                new LinkDto(CreateUsersResourceUri(
                usersResourceParameters, ResourceUriType.PreviousPage)
                , "previousPage", "GET"));
            }

            return links;
        }


    }
}
