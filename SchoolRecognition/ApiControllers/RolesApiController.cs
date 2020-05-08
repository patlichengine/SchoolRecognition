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
    public class RolesApiController : ControllerBase
    {
        private readonly IRolesRepository _rolesRepository;

        public RolesApiController(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository ??
                throw new ArgumentNullException(nameof(rolesRepository));
        }
        // GET: api/RolesApi
        [HttpGet]
        public ActionResult<IEnumerable<RolesDto>> Get()
        {
            var result = _rolesRepository.GetRoles().Result;
            return Ok(result);
        }

        // GET: api/RolesApi/5
        [HttpGet]
        [Route("{roleId}")]
        public IActionResult Get(Guid roleId)
        {
            if(roleId == Guid.Empty)
            {
                return NotFound();
            }

            var result = _rolesRepository.GetRole(roleId);
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: api/RolesApi
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/RolesApi/5
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
