using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolRecognition.Models;
using SchoolRecognition.Services;

namespace SchoolRecognition.ApiControllers
{
    [Route("api/offices")]
    [ApiController]
    public class OfficesApiController : ControllerBase
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly ILogger<OfficesApiController> _logger;

        public OfficesApiController(IOfficeRepository officeRepository, ILogger<OfficesApiController> logger)
        {
            _officeRepository = officeRepository ?? throw new ArgumentNullException(nameof(officeRepository));
        }

        // GET: api/offices
        [HttpGet]
        public ActionResult<IEnumerable<OfficesDto>> GetOffices()
        {
            var result = _officeRepository.GetOffices().Result;
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

            var result = _officeRepository.GetOffice(officeId).Result;

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

            var result = _officeRepository.GetOfficeSchools(officeId).Result;

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

            var result = _officeRepository.GetOfficeSchools(officeId, centreId).Result;

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

            var result = _officeRepository.GetOfficeSchools(officeId, schoolId).Result;

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

            var result = _officeRepository.GetOfficeCentres(officeId).Result;

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        
    }
}
