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
using SchoolRecognition.Helpers;
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

                var states = await _statesRepository.List();

                return PartialView(states);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        //Get id

        [Route("details/{id:guid}")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Details(Guid id, string orderBy, string searchQuery, int? pageNumber)
        {
            try
            {

                if (id == Guid.Empty)
                {
                    return BadRequest();
                }



                IEnumerable<int> pages = new List<int>();
                IEnumerable<OfficeStatesViewDto> officeStates = new List<OfficeStatesViewDto>();

                var resourceParams = new LocalGovernmentsResourceParams()
                {
                    PageSize = 5,
                    PageNumber = pageNumber != null ? pageNumber.Value : 1,
                    SearchQuery = !String.IsNullOrWhiteSpace(searchQuery) ? searchQuery : null,
                    OrderBy = !String.IsNullOrWhiteSpace(orderBy) ? searchQuery : "LgaName",
                };
                //Instantiate PagedList
                PagedList<LocalGovernmentsViewDto> lgas = PagedList<LocalGovernmentsViewDto>
                         .Create(Enumerable.Empty<LocalGovernmentsViewDto>().AsQueryable(),
                             resourceParams.PageNumber,
                             resourceParams.PageSize);
                switch (orderBy)
                {
                    case "name_desc":
                        resourceParams.OrderBy = "LgaNameDesc";
                        break;
                    case "name":
                        resourceParams.OrderBy = "LgaName";
                        break;
                    case "code_desc":
                        resourceParams.OrderBy = "LgaCodeDesc";
                        break;
                    case "code":
                        resourceParams.OrderBy = "LgaCode";
                        break;
                    default:
                        resourceParams.OrderBy = "LgaName";
                        break;
                }

                var result = await _statesRepository.GetIncludingPagedListOfLocalGovernments(id, resourceParams);

                if (result == null)
                {
                    return NotFound();
                }
                else if (result != null)
                {
                    var totalPages = result.StateLGAs.TotalPages;
                    officeStates = result.StateOfficeStates != null ? result.StateOfficeStates : new List<OfficeStatesViewDto>();

                    pages = Enumerable.Range(1, totalPages);

                    if (totalPages > 5)
                    {

                        if ((resourceParams.PageNumber + 2) >= totalPages)
                        {
                            pages = pages.Skip(totalPages - 5).Take(5).ToList();
                        }
                        else if ((resourceParams.PageNumber - 2) <= 1)
                        {
                            pages = pages.Take(5).ToList();
                        }
                        else
                        {
                            pages = pages.Skip(resourceParams.PageNumber - 2).Take(5).ToList();
                        }
                    }
                }

                ViewData["Pages"] = pages;
                ViewData["State"] = result;
                ViewData["OfficeStates"] = officeStates;
                ViewData["OrderBy"] = orderBy;
                ViewData["SearchQuery"] = searchQuery;

                lgas = result.StateLGAs;

                return PartialView(lgas);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }




        [Route("new_state")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Create()
        {
            return PartialView();
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
                    if (await _statesRepository.Exists(model.StateName))
                    {

                        _flashMessage.Danger("Duplicate Data Entry!", "An State with the same description already exists in the system...");
                        return PartialView(model);
                    }

                    var result = await _statesRepository.Create(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "New State Added Successfully!");
                        return RedirectToAction("Index", "States");
                    }
                    else
                    {
                        _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly...");
                        return PartialView(model);
                    }
                }
                _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly...");
                return PartialView(model);
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

                var state = await _statesRepository.Get(id.Value);
                var model = _mapper.Map<StatesCreateDto>(state);
                return PartialView(model);
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
                    if (await _statesRepository.Exists(model.Id, model.StateName))
                    {

                        _flashMessage.Danger("Duplicate Data Entry!", "An State with the same description already exists in the system...");
                        return PartialView(model);
                    }

                    //Set Pins as active 
                    var result = await _statesRepository.Update(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "State Updated Successfully!");
                        return RedirectToAction("Index", "States");
                    }
                    else
                    {
                        _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly...");
                        return PartialView(model);
                    }
                }
                _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly...");
                return PartialView(model);
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

                var state = await _statesRepository.Get(id.Value);

                if (state == null)
                {
                    return NotFound();
                }




                return PartialView(state);
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
                    await _statesRepository.Delete(model.Id);

                    _flashMessage.Info("Delete Successful", "State removed from system!");
                    return RedirectToAction("Index", "States");
                }
                _flashMessage.Danger("Oops...Something went wrong!", "Invalid operation parameters!");
                return PartialView(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
