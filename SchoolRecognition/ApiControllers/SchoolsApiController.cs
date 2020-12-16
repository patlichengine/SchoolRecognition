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
    [Route("api/Schools")]
    [ApiController]
    public class SchoolsApiController : ControllerBase
    {
        private readonly ISchoolsRepository _schoolsRepository;

        public SchoolsApiController(ISchoolsRepository schoolsRepository)
        {
            _schoolsRepository = schoolsRepository;
        }


        // GET: api/Schools
        [HttpGet]
        public ActionResult<IEnumerable<SchoolsViewDto>> Get()
        {
            var result = _schoolsRepository.List().Result;

            return Ok(result);
        }

        // GET: api/Schools/5
        [HttpGet("{id}", Name = "GetSchool")]
        public ActionResult Get(Guid schoolId)
        {
            if (schoolId == Guid.Empty)
            {
                return NotFound();
            }

            var result = _schoolsRepository.Get(schoolId).Result;
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/Schools
        [HttpPost]
        public ActionResult<SchoolsCreateDto> Post(SchoolsCreateDto createSchoolsDto)
        {
            var result = _schoolsRepository.Create(createSchoolsDto).Result;
            //Return the named user using the specified URI name

            var final = CreatedAtRoute("GetSchool",
                new { userId = result }, result);
            return Ok(final);
        }

        // PUT: api/Schools/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, SchoolsCreateDto updateSchoolsDto)
        {


            var result = _schoolsRepository.Update(updateSchoolsDto).Result;

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid schoolId)
        {
            var result = _schoolsRepository.Delete(schoolId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
