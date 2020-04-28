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
    public class RecognitionTypesApiController : ControllerBase
    {

        private readonly IRecognitionTypesRepository _recognitionTypesRepository;

        public RecognitionTypesApiController(IRecognitionTypesRepository recognitionTypesRepository)
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
                var result = await _recognitionTypesRepository.GetAll();
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
                    return NotFound();
                }

                var result = await _recognitionTypesRepository.GetDetailsAndIncludePins(recognitionTypeId, _pageNumber);
                if (result == null)
                {
                    return NotFound();
                }

                var recognitionTypesDto = new RecognitionTypeApiViewDto()
                {
                    Id = result.Id,
                    RecognitionTypeCode = result.RecognitionTypeCode,
                    RecognitionTypeName = result.RecognitionTypeName,
                    RecognitionTypePins = (result.RecognitionTypePins != null ? result.RecognitionTypePins.Entitys : new List<PinsViewDto>()),
                    RangeFrom = (result.RecognitionTypePins != null ? result.RecognitionTypePins.LowerLimit + 1 : 0),
                    RangeTo = (result.RecognitionTypePins != null ? result.RecognitionTypePins.UpperLimit : 0),
                    RangeTotalPins = (result.RecognitionTypePins != null ? result.RecognitionTypePins.TotalDBEntitysCount : 0),
                };

                return Ok(recognitionTypesDto);
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

                if (recognitionTypeId == Guid.Empty)
                {
                    return NotFound();
                }

                var result = await _recognitionTypesRepository.GetDetailsAndIncludePins(recognitionTypeId, _pageNumber);
                if (result == null)
                {
                    return NotFound();
                }

                var recognitionTypesDto = new RecognitionTypeApiViewDto()
                {
                    Id = result.Id,
                    RecognitionTypeCode = result.RecognitionTypeCode,
                    RecognitionTypeName = result.RecognitionTypeName,
                    RecognitionTypePins = (result.RecognitionTypePins != null ? result.RecognitionTypePins.Entitys : new List<PinsViewDto>()),
                    RangeFrom = (result.RecognitionTypePins != null ? result.RecognitionTypePins.LowerLimit + 1 : 0),
                    RangeTo = (result.RecognitionTypePins != null ? result.RecognitionTypePins.UpperLimit : 0),
                    RangeTotalPins = (result.RecognitionTypePins != null ? result.RecognitionTypePins.TotalDBEntitysCount : 0),
                };

                return Ok(recognitionTypesDto);
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