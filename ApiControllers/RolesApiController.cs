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
    [Route("api/roles")]
    [ApiController]
    public class ApplicationRolesApiController : ControllerBase
    {
        private readonly IApplicationRolesRepository _rolesRepository;

        public ApplicationRolesApiController(IApplicationRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository ??
                throw new ArgumentNullException(nameof(rolesRepository));
        }
        // GET: api/ApplicationRolesApi
        [HttpGet]
        public ActionResult<IEnumerable<ApplicationRolesDto>> Get()
        {
            var result = _rolesRepository.GetApplicationRoles().Result;
            return Ok(result);
        }

        // GET: api/ApplicationRolesApi/5
        [HttpGet]
        [Route("{roleId}")]
        public IActionResult Get(Guid roleId)
        {
            if(roleId == Guid.Empty)
            {
                return NotFound();
            }

            var result = _rolesRepository.GetApplicationRole(roleId);
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: api/ApplicationRolesApi
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/ApplicationRolesApi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
