using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SchoolRecognition.Models;
using SchoolRecognition.Services;

namespace SchoolRecognition.ApiControllers
{
    [Route("api/recognitionTypes")]
    [ApiController]
    public class RecognitionTypesApiController : ControllerBase
    {

        private readonly IRecognitionTypesRepository _recognitionTypesRepository;
        private readonly LinkGenerator _linkGenerator;

        public RecognitionTypesApiController(IRecognitionTypesRepository recognitionTypesRepository, LinkGenerator linkGenerator)
        {
            _recognitionTypesRepository = recognitionTypesRepository ??
                throw new ArgumentNullException(nameof(recognitionTypesRepository));
            _linkGenerator = linkGenerator;
        }
        // GET: api/RecognitionTypesApi
        [HttpGet]
        public async Task<ActionResult> Get()
        {           
            try
            {
                var result = await _recognitionTypesRepository.GetAllRecognitionTypesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET: api/RecognitionTypesApi/5
        [HttpGet]
        [Route("{recognitionTypeId}")]
        public async Task<IActionResult> Get(Guid recognitionTypeId)
        {

            int _pageNumber = 0;
            try
            {
                if (recognitionTypeId == Guid.Empty)
                {
                    return BadRequest();
                }

                var result = await _recognitionTypesRepository.GetRecognitionTypesSingleOrDefaultAsync(recognitionTypeId);

                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "The requested resource could not be found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        

        // GET: api/RecognitionTypesApi/5
        [HttpGet]
        [Route("{recognitionTypeId}/pins/{pageNumber}")]
        public async Task<IActionResult> Get(Guid recognitionTypeId, int? pageNumber)
        {
            int _pageNumber = 0;
            try
            {
                if (recognitionTypeId == Guid.Empty)
                {
                    return BadRequest();
                }

                var result = await _recognitionTypesRepository.GetRecognitionTypesSingleOrDefaultAsync(recognitionTypeId);

                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "The requested resource could not be found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // POST: api/RecognitionTypesApi
        [HttpPost]
        public async Task<IActionResult> Post(RecognitionTypesCreateDto model)
        {
            
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                var result = await _recognitionTypesRepository.CreateRecognitionTypeAsync(model);
                if (result == null)
                {
                    return BadRequest();
                }

                var location = _linkGenerator.GetPathByAction("Get", "RecognitionTypesApi", new { recognitionTypeId = result });
                return Created(location, result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        

        // PUT: api/RecognitionTypesApi
        [HttpPut]
        public async Task<IActionResult> Put(RecognitionTypesCreateDto model)
        {
            
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }

                var result = await _recognitionTypesRepository.UpdateRecognitionTypeAsync(model);
                if (result == null)
                {
                    return BadRequest();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        // PUT: api/RecognitionTypesApi
        [HttpDelete]
        [Route("{recognitionTypeId}")]
        public async Task<IActionResult> Delete(Guid recognitionTypeId)
        {
            
            try
            {
                if (recognitionTypeId == Guid.Empty)
                {
                    return NotFound();
                }

                await _recognitionTypesRepository.DeleteRecognitionTypeAsync(recognitionTypeId);

                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


    }
}