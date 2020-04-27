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
                int _pageNumber = 0;

                var result = await _pinsRepository.Get(_pageNumber);
                if (result == null)
                {
                    return NotFound();
                }
                var pinsApiListViewDto = new PinsApiListViewDto()
                {
                    RecognitionTypePins = result.Entitys,
                    RangeFrom = (result.LowerLimit + 1),
                    RangeTo = result.UpperLimit,
                    RangeTotalPins = result.TotalDBEntitysCount
                };
                return Ok(pinsApiListViewDto);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        // GET: api/PinsApi/5
        [HttpGet]
        [Route("{pageNumber}")]
        public async Task<IActionResult> Get(int? pageNumber)
        {            

            try
            {
                int _pageNumber = 0;

                //Setting page number
                if (pageNumber != null && pageNumber.Value >= 0)
                {
                    _pageNumber = pageNumber.Value;
                }

                if (_pageNumber > 0)
                {
                    _pageNumber = _pageNumber - 1;
                }

                var result = await _pinsRepository.Get(_pageNumber);

                if (result == null)
                {
                    return NotFound();
                }
                var pinsApiListViewDto = new PinsApiListViewDto()
                {
                    RecognitionTypePins = result.Entitys,
                    RangeFrom = (result.LowerLimit + 1),
                    RangeTo = result.UpperLimit,
                    RangeTotalPins = result.TotalDBEntitysCount
                };
                return Ok(pinsApiListViewDto);
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
                    return NotFound();
                }

                var result = await _pinsRepository.Get(pinId);
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

                var result = await _pinsRepository.CreateSeveralPins(model);
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

                var result = await _pinsRepository.Update(model);
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


        // DELETE: api/PinsApi/5
        [HttpDelete]
        [Route("{pinId}")]
        public async Task<IActionResult> Delete(Guid pinId)
        {

            try
            {
                if (pinId == Guid.Empty)
                {
                    return NotFound();
                }

                await _pinsRepository.Delete(pinId);

                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }




    }
}