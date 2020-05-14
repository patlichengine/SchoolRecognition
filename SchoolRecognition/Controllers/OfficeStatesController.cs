using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolRecognition.Models;
using SchoolRecognition.Services;
using Vereyon.Web;


namespace SchoolRecognition.Controllers
{

    [Route("manage_offices/office_to_state_assignation")]
    public class OfficeStatesController : Controller
    {
        private IFlashMessage _flashMessage;
        private IOfficesRepository _officesRepository;
        private IOfficeStatesRepository _officeStatesRepository;
        private IStatesRepository _statesRepository;
        private readonly IMapper _mapper;

        public OfficeStatesController(IFlashMessage flashMessage, IOfficesRepository officesRepository, IOfficeStatesRepository officeStatesRepository, IStatesRepository statesRepository, IMapper mapper)
        {
            _flashMessage = flashMessage ??
                throw new ArgumentNullException(nameof(mapper));
            _officesRepository = officesRepository ??
               throw new ArgumentNullException(nameof(officesRepository));
            _officeStatesRepository = officeStatesRepository ??
               throw new ArgumentNullException(nameof(officesRepository));
            _statesRepository = statesRepository ??
               throw new ArgumentNullException(nameof(statesRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }


        [Route("reassign_office/{officeId:guid}")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Update(Guid? officeId)
        {
            try
            {
                var states = await _statesRepository.GetAllStatesAsync();
                //


                if (officeId == null)
                {
                    return NotFound();
                }

                var officeState = await _officeStatesRepository.GetOfficeStatesByOfficeIdSingleOrDefaultAsync(officeId.Value);

                if (officeState == null)
                {
                    return BadRequest();
                }
                var model = new OfficeStatesCreateDto()
                {
                    Id = officeState.Id,
                    StateId = officeState.StateId,
                    OfficeId = officeState.OfficeId,
                    OfficeName = officeState.OfficeName,
                    OfficeAddress = officeState.OfficeAddress
                };

                ViewData["States"] = states.OrderBy(x => x.StateCode).Select(x =>
                 new SelectListItem()
                 {
                     Text = $"{x.StateCode} {x.StateName}",
                     Value = x.Id.ToString(),
                     Selected = x.Id == model.StateId
                 }).ToList();

                return View(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [Route("reassign_office/{officeId:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(OfficeStatesCreateDto model)
        {

            try
            {

                var states = await _statesRepository.GetAllStatesAsync();
                //
                ViewData["States"] = states.OrderBy(x => x.StateCode).Select(x =>
                 new SelectListItem()
                 {
                     Text = $"{x.StateCode} {x.StateName}",
                     Value = x.Id.ToString(),
                 }).ToList();


                if (ModelState.IsValid)
                {

                    //Check if entry with similar data already exists
                    if (await _officeStatesRepository.CheckIfOfficeStateExists(model.Id, model.StateId, model.OfficeId))
                    {

                        _flashMessage.Danger("Duplicate Data Entry!", "An Office with the same Name already exists in the system...");
                        return View(model);
                    }

                    //Set Pins as active 
                    var result = await _officeStatesRepository.UpdateOfficeStateAsync(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "Office Re-assigned Successfully!");
                        return RedirectToAction("Details", "Offices", new { id = model.OfficeId });
                    }
                    else
                    {
                        _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly...");
                        return View(model);
                    }
                }
                _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly...");
                return View(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}