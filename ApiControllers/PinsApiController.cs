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

        private readonly IPinsRepository _pinsRepository;

        public PinsApiController(IPinsRepository pinsRepository)
        {
            _pinsRepository = pinsRepository ??
                throw new ArgumentNullException(nameof(pinsRepository));
        }
        // GET: api/PinsApi
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            
            try
            {

                var result = await _pinsRepository.GetAllPinsAsync();
       
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

                

        // GET: api/PinsApi/5/details
        [HttpGet]
        [Route("{pinId}/details")]
        public async Task<IActionResult> Get(Guid pinId)
        {            

            try
            {
                if (pinId == Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Invalid request parameters!");
                }

                var result = await _pinsRepository.GetPinsSingleOrDefaultAsync(pinId);
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


        // POST: api/PinsApi
        [HttpPost]
        public async Task<IActionResult> Post(PinsCreateDto model)
        {

            try
            {
                if (model == null)
                {
                    return BadRequest();
                }

                var result = await _pinsRepository.CreateMultiplePinsAsync(model);
                if (result == false)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        

        // PUT: api/PinsApi
        [HttpPut]
        public async Task<IActionResult> Put(PinsUpdateDto model)
        {          

            try
            {
                if (model == null)
                {
                    return BadRequest();
                }

                var result = await _pinsRepository.UpdatePinAsync(model);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        // DELETE: api/PinsApi/5
        [HttpDelete]
        [Route("{pinId}")]
        public async Task<IActionResult> Delete(Guid pinId)
        {

            try
            {
                if (pinId == Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Invalid request parameters!");
                }

                await _pinsRepository.DeletePinAsync(pinId);

                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }




    }
}