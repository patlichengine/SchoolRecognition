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
        public async Task<ActionResult> Get()
        {           
            try
            {
                var result = await _recognitionTypesRepository.Get();
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
            
            try
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
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // POST: api/RecognitionTypesApi
        [HttpPost]
        public async Task<IActionResult> Post(RecognitionTypesDto model)
        {
            
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }

                var result = await _recognitionTypesRepository.Create(model);
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
        [HttpPut]
        public async Task<IActionResult> Put(RecognitionTypesDto model)
        {
            
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }

                var result = await _recognitionTypesRepository.Update(model);
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

                await _recognitionTypesRepository.Delete(recognitionTypeId);

                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


    }
}