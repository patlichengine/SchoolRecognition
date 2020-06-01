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

    [Route("manage_offices/council_offices")]
    public class OfficesController : Controller
    {
        private IFlashMessage _flashMessage;
        private IOfficesRepository _officesRepository;
        private IOfficeTypesRepository _officeTypesRepository;
        private readonly IMapper _mapper;

        public OfficesController(IFlashMessage flashMessage, IOfficesRepository officesRepository, IOfficeTypesRepository officeTypesRepository, IMapper mapper)
        {
            _flashMessage = flashMessage ??
                throw new ArgumentNullException(nameof(mapper));
            _officesRepository = officesRepository ??
               throw new ArgumentNullException(nameof(officesRepository));
            _officeTypesRepository = officeTypesRepository ??
               throw new ArgumentNullException(nameof(officesRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        // GET: Offices
        [Route("")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Index(string orderBy, string searchQuery, int? pageNumber)
        {
            try
            {



                IEnumerable<int> pages = new List<int>();

                var resourceParams = new OfficesResourceParams()
                {
                    PageNumber = pageNumber != null ? pageNumber.Value : 1,
                    SearchQuery = !String.IsNullOrWhiteSpace(searchQuery) ? searchQuery : null,
                    OrderBy = !String.IsNullOrWhiteSpace(orderBy) ? searchQuery : "DateCreated",
                };
                //Instantiate PagedList
                PagedList<OfficesViewDto> offices = PagedList<OfficesViewDto>
                         .Create(Enumerable.Empty<OfficesViewDto>().AsQueryable(),
                             resourceParams.PageNumber,
                             resourceParams.PageSize);
                switch (orderBy)
                {
                    case "date_desc":
                        resourceParams.OrderBy = "DateCreatedDesc";
                        break;
                    case "date":
                        resourceParams.OrderBy = "DateCreated";
                        break;
                    case "office_name_desc":
                        resourceParams.OrderBy = "OfficeNameDesc";
                        break;
                    case "office_name":
                        resourceParams.OrderBy = "OfficeName";
                        break;
                    default:
                        resourceParams.OrderBy = "DateCreated";
                        break;
                }

                var result = await _officesRepository.PagedList(resourceParams);

                if (result != null)
                {
                    var totalPages = result.TotalPages;

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

                offices = result;

                ViewData["Pages"] = pages;
                ViewData["OrderBy"] = orderBy;
                ViewData["SearchQuery"] = searchQuery;


                return PartialView(offices);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [Route("details/{id:guid}")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        // GET: Offices/Details/5
        public async Task<IActionResult> Details(Guid id, string orderBy, string searchQuery, int? pageNumber)
        {
            try
            {

                if (id == Guid.Empty)
                {
                    return BadRequest();
                }



                IEnumerable<int> pages = new List<int>();

                var resourceParams = new SchoolsResourceParams()
                {
                    PageNumber = pageNumber != null ? pageNumber.Value : 1,
                    SearchQuery = !String.IsNullOrWhiteSpace(searchQuery) ? searchQuery : null,
                    OrderBy = !String.IsNullOrWhiteSpace(orderBy) ? searchQuery : "Name",
                };
                //Instantiate PagedList
                PagedList<SchoolsViewDto> pins = PagedList<SchoolsViewDto>
                         .Create(Enumerable.Empty<SchoolsViewDto>().AsQueryable(),
                             resourceParams.PageNumber,
                             resourceParams.PageSize);
                switch (orderBy)
                {
                    case "year_desc":
                        resourceParams.OrderBy = "YearEstablishedDesc";
                        break;
                    case "year":
                        resourceParams.OrderBy = "YearEstablished";
                        break;
                    case "school_name_desc":
                        resourceParams.OrderBy = "SchoolNameDesc";
                        break;
                    case "school_name":
                        resourceParams.OrderBy = "SchoolName";
                        break;
                    default:
                        resourceParams.OrderBy = "YearEstablished";
                        break;
                }

                var result = await _officesRepository.GetIncludingPagedListOfSchools(id, resourceParams);

                if (result != null)
                {
                    var totalPages = result.OfficeSchools.TotalPages;

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
                ViewData["Office"] = result;
                ViewData["OrderBy"] = orderBy;
                ViewData["SearchQuery"] = searchQuery;

                pins = result.OfficeSchools;

                return PartialView(pins);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: Offices/Create
        [Route("new_office")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Create()
        {
            try
            {
                //
                var creationDependencys = await _officesRepository.GetCreationDependencys();
                var officeTypes = creationDependencys.OfficeTypes;
                var states = creationDependencys.States;
                //
                ViewData["OfficeTypes"] = officeTypes.OrderBy(x => x.TypeDescription).Select(x =>
                 new SelectListItem()
                 {
                     Text = x.TypeDescription,
                     Value = x.Id.ToString(),
                 }).ToList();
                //
                ViewData["States"] = states.OrderBy(x => x.StateCode).Select(x =>
                 new SelectListItem()
                 {
                     Text = $"{x.StateCode} {x.StateName}",
                     Value = x.Id.ToString(),
                 }).ToList();


                return PartialView();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("new_office")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfficesCreateDto model)
        {

            try
            {
                var creationDependencys = await _officesRepository.GetCreationDependencys();
                var officeTypes = creationDependencys.OfficeTypes;
                var states = creationDependencys.States;
                //
                ViewData["OfficeTypes"] = officeTypes.OrderBy(x => x.TypeDescription).Select(x =>
                 new SelectListItem()
                 {
                     Text = x.TypeDescription,
                     Value = x.Id.ToString(),
                 }).ToList();
                //
                ViewData["States"] = states.OrderBy(x => x.StateCode).Select(x =>
                 new SelectListItem()
                 {
                     Text = $"{x.StateCode} {x.StateName}",
                     Value = x.Id.ToString(),
                 }).ToList();

                var url = Url.Action("Create");

                if (ModelState.IsValid)
                {


                    //Check if entry with similar data already exists
                    if (await _officesRepository.Exists(model.OfficeName))
                    {

                        _flashMessage.Danger("Duplicate Data Entry!", "An Office with the same Name already exists in the system..."); 
                        return Json(url);
                    }

                    var result = await _officesRepository.Create(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "New Office Added Successfully!");
                        url = Url.Action("ViewOffices", "OfficeTypes", new { id = model.OfficeTypeId });
                    }
                    else
                    {
                        _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly..."); 
                        return Json(url);
                    }
                }
                _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly..."); 
                return Json(url);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: Offices/Edit/5

        [Route("update/{id?}")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Update(Guid? id)
        {
            try
            {
                var creationDependencys = await _officesRepository.GetCreationDependencys();
                var officeTypes = creationDependencys.OfficeTypes;
                var states = creationDependencys.States;
                //


                if (id == null)
                {
                    return NotFound();
                }

                var office = await _officesRepository.Get(id.Value);

                if (office == null)
                {
                    return BadRequest();
                }
                var model = new OfficesCreateDto()
                {
                    Id = office.Id,
                    OfficeName = office.OfficeName,
                    OfficeAddress = office.OfficeAddress,
                    StateId = office.StateId,
                    DateCreated = office.DateCreated,
                    CreatedBy = office.CreatedBy,
                    OfficeImage = office.OfficeImage,
                    Longitute = office.Longitude,
                    Latitude = office.Latitude,
                    OfficeTypeId = office.OfficeTypeId
                };


                ViewData["OfficeTypes"] = officeTypes.OrderBy(x => x.TypeDescription).Select(x =>
                 new SelectListItem()
                 {
                     Text = x.TypeDescription,
                     Value = x.Id.ToString(),
                     Selected = x.Id == model.OfficeTypeId
                 }).ToList();
                //
                ViewData["States"] = states.OrderBy(x => x.StateCode).Select(x =>
                 new SelectListItem()
                 {
                     Text = $"{x.StateCode} {x.StateName}",
                     Value = x.Id.ToString(),
                     Selected = x.Id == model.StateId
                 }).ToList();

                return PartialView(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [Route("update/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(OfficesCreateDto model)
        {

            try
            {

                var creationDependencys = await _officesRepository.GetCreationDependencys();
                var officeTypes = creationDependencys.OfficeTypes;
                var states = creationDependencys.States;
                //
                ViewData["OfficeTypes"] = officeTypes.OrderBy(x => x.TypeDescription).Select(x =>
                 new SelectListItem()
                 {
                     Text = x.TypeDescription,
                     Value = x.Id.ToString(),
                 }).ToList();
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
                    if (await _officesRepository.Exists(model.Id, model.OfficeName))
                    {

                        _flashMessage.Danger("Duplicate Data Entry!", "An Office with the same Name already exists in the system...");
                        return PartialView(model);
                    }

                    //Set Pins as active 
                    var result = await _officesRepository.Update(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "Office Updated Successfully!");
                        return RedirectToAction("ViewOffices", "OfficeTypes", new { id = model.OfficeTypeId });
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

        // GET: Offices/Delete/5
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

                var office = await _officesRepository.Get(id.Value);

                if (office == null)
                {
                    return NotFound();
                }




                return PartialView(office);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: Offices/Delete/5
        [Route("delete/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(OfficesViewDto model)
        {
            try
            {
                var url = Url.Action("Delete", new { id = model.Id });
                if (ModelState.IsValid)
                {
                    await _officesRepository.Delete(model.Id);

                    _flashMessage.Info("Delete Successful", "Office removed from system!");
                    url = Url.Action("Index", "Offices");
                    return Json(url);
                }
                _flashMessage.Danger("Oops...Something went wrong!", "Invalid operation parameters!");
                return Json(url);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
