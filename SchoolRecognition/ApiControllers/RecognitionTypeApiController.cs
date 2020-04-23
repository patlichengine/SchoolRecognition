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
    [Route("api/recognitionTypes")]
    [ApiController]
    public class RecognitionTypeApiController : ControllerBase
    {

        private readonly IRecognitionTypesRepository _recognitionTypesRepository;

        public RecognitionTypeApiController(IRecognitionTypesRepository recognitionTypesRepository)
        {
            _recognitionTypesRepository = recognitionTypesRepository ??
                throw new ArgumentNullException(nameof(recognitionTypesRepository));
        }
        // GET: api/RecognitionTypesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecognitionTypesDto>>> Get()
        {
            var result = await _recognitionTypesRepository.Get();
            return Ok(result);
        }

        // GET: api/RecognitionTypesApi/5
        [HttpGet]
        [Route("{recognitionTypeId}")]
        public async Task<IActionResult> Get(Guid recognitionTypeId)
        {
            if (recognitionTypeId == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _recognitionTypesRepository.Get(recognitionTypeId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


    }
}