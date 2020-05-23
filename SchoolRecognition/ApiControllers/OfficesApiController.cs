using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.Models;
using SchoolRecognition.Services;

namespace SchoolRecognition.ApiControllers
{
    [Route("api/offices")]
    [ApiController]
    public class OfficesApiController : ControllerBase
    {
        private readonly IOfficesRepository _officeRepository;

        public OfficesApiController(IOfficesRepository officeRepository)
        {
            _officeRepository = officeRepository ?? throw new ArgumentNullException(nameof(officeRepository));
        }

        // GET: api/offices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfficesViewDto>>> GetOffices()
        {
            var result = await _officeRepository.GetAllOfficesAsync();
            return Ok(result);
        }

        // GET: api/offices/5
        [HttpGet("{officeId:guid}")]
        public IActionResult GetOffice(Guid officeId)
        {
            if(officeId == Guid.Empty)
            {
                return NotFound();
            }

            var result = _officeRepository.GetOfficesSingleOrDefaultAsync(officeId).Result;

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/offices/5
        [HttpGet("{officeId:guid}/schools")]
        public IActionResult GetOfficeSchools(Guid officeId)
        {
            if (officeId == Guid.Empty)
            {
                return NotFound();
            }

            var result = _officeRepository.GetOfficesAllSchoolsAsync(officeId).Result;

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{officeId:guid}/schools/{centreId:guid}")]
        public IActionResult GetOfficeCentres(Guid officeId, Guid centreId)
        {
            if (officeId == Guid.Empty)
            {
                return NotFound();
            }

            if (centreId == Guid.Empty)
            {
                return NotFound();
            }

            var result = _officeRepository.GetOfficesAllSchoolsAsync(officeId).Result;
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{officeId:guid}/schools/{schoolId:guid}")]
        public IActionResult GetOfficeSchools(Guid officeId, Guid schoolId)
        {
            if (officeId == Guid.Empty)
            {
                return NotFound();
            }

            if (schoolId == Guid.Empty)
            {
                return NotFound();
            }

            var result = _officeRepository.GetOfficesAllSchoolsAsync(officeId).Result;

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/offices/5
        [HttpGet("{officeId}/centres")]
        public IActionResult GetOfficeCentres(Guid officeId)
        {
            if (officeId == Guid.Empty)
            {
                return NotFound();
            }

            var result = _officeRepository.GetOfficesAllSchoolsAsync(officeId).Result;

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        
    }
}
