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
    [Route("api/pins")]
    [ApiController]
    public class PinsApiController : ControllerBase
    {

        private readonly IPinsRepository _recognitionTypesRepository;

        public PinsApiController(IPinsRepository recognitionTypesRepository)
        {
            _recognitionTypesRepository = recognitionTypesRepository ??
                throw new ArgumentNullException(nameof(recognitionTypesRepository));
        }
        // GET: api/PinsApi
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


        // GET: api/PinsApi/5
        [HttpGet]
        [Route("{pinId}")]
        public async Task<IActionResult> Get(Guid pinId)
        {            

            try
            {
                if (pinId == Guid.Empty)
                {
                    return NotFound();
                }

                var result = await _recognitionTypesRepository.Get(pinId);
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

        // GET: api/PinsApi/5
        [HttpPost]
        public async Task<IActionResult> Post(PinsCreateDto model)
        {          

            try
            {
                if (model == null)
                {
                    return BadRequest();
                }

                var result = await _recognitionTypesRepository.CreateSeveralPins(model);
                if (result == false)
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




    }
}