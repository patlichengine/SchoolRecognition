using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.DbContexts;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using SchoolRecognition.Services;
using Vereyon.Web;

namespace SchoolRecognition.Controllers
{

    [Route("manage_locations/states")]
    public class StatesController : Controller
    {
        private IFlashMessage _flashMessage;
        private IStatesRepository _statesRepository;
        private readonly IMapper _mapper;

        public StatesController(IFlashMessage flashMessage, IStatesRepository statesRepository, IMapper mapper)
        {
            _flashMessage = flashMessage ??
                throw new ArgumentNullException(nameof(mapper));
            _statesRepository = statesRepository ??
               throw new ArgumentNullException(nameof(statesRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [Route("")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Index()
        {
            try
            {

                var states = await _statesRepository.GetAllStatesAsync();

                return View(states);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        //Get id

        [Route("new_state")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: States/Create
        [Route("new_state")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StatesCreateDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //Check if entry with similar data already exists
                    if (await _statesRepository.CheckIfStateExists(model.StateName))
                    {

                        _flashMessage.Danger("Duplicate Data Entry!", "An State with the same description already exists in the system...");
                        return View(model);
                    }

                    var result = await _statesRepository.CreateStateAsync(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "New State Added Successfully!");
                        return RedirectToAction("Index", "States");
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


        // GET: States/Create
        [Route("new_state")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> CreateState()
        {
            //
            
            return View();
        }

        [Route("new_state")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateState(StatesCreateDto model)
        {

            try
            {

                if (ModelState.IsValid)
                {


                    //Check if entry with similar data already exists
                    if (await _statesRepository.CheckIfStateExists(model.StateName))
                    {

                        _flashMessage.Danger("Duplicate Data Entry!", "An State with the same Name already exists in the system...");
                        return View(model);
                    }
                    //model.Id = Guid.Empty;
                    var result = await _statesRepository.CreateStateAsync(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "New State Added Successfully!");
                        return RedirectToAction("Index", "States");
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



        [Route("update")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Update(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var state = await _statesRepository.GetStatesSingleOrDefaultAsync(id.Value);
                var model = _mapper.Map<StatesCreateDto>(state);
                return View(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [Route("update")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(StatesCreateDto model)
        {

            try
            {
                if (ModelState.IsValid)
                {


                    //Check if entry with similar data already exists
                    if (await _statesRepository.CheckIfStateExists(model.Id, model.StateName))
                    {

                        _flashMessage.Danger("Duplicate Data Entry!", "An State with the same description already exists in the system...");
                        return View(model);
                    }

                    //Set Pins as active 
                    var result = await _statesRepository.UpdateStateAsync(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "State Updated Successfully!");
                        return RedirectToAction("ViewPins", "States", new { id = model.Id });
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

        // GET: States/Delete/5
        [Route("delete/{id?}")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Delete(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var state = await _statesRepository.GetStatesSingleOrDefaultAsync(id.Value);

                if (state == null)
                {
                    return NotFound();
                }




                return View(state);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: States/Delete/5
        [Route("delete/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(StatesViewDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _statesRepository.DeleteStateAsync(model.Id);

                    _flashMessage.Info("Delete Successful", "State removed from system!");
                    return RedirectToAction("Index", "States");
                }
                _flashMessage.Danger("Oops...Something went wrong!", "Invalid operation parameters!");
                return View(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
