using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using SchoolRecognition.Services;
using Vereyon.Web;

namespace SchoolRecognition.Controllers
{

    [Route("manage_offices")]
    public class OfficeTypesController : Controller
    {

        private IFlashMessage _flashMessage;
        private IOfficesRepository _officesRepository;
        private IOfficeTypesRepository _officeTypesRepository;
        private readonly IMapper _mapper;

        public OfficeTypesController(IFlashMessage flashMessage, IOfficesRepository officesRepository, IOfficeTypesRepository officeTypesRepository, IMapper mapper)
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

        [Route("")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Index()
        {
            try
            {

                var officeTypes = await _officeTypesRepository.List();

                return PartialView(officeTypes);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        //Get id
        [Route("office_type/{id:guid}/offices")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> ViewOffices(Guid id, string orderBy, string searchQuery, int? pageNumber)
        {
            try
            {

                if (id == Guid.Empty)
                {
                    return BadRequest();
                }



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
                    case "office_name_desc":
                        resourceParams.OrderBy = "OfficeNameDesc";
                        break;
                    case "office_name":
                        resourceParams.OrderBy = "OfficeName";
                        break;
                    default:
                        resourceParams.OrderBy = "OfficeName";
                        break;
                }

                var result = await _officeTypesRepository.GetIncludingPagedListOfOffices(id, resourceParams);

                if (result != null)
                {
                    var totalPages = result.OfficeTypeOffices.TotalPages;

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
                ViewData["OfficeType"] = result;
                ViewData["OrderBy"] = orderBy;
                ViewData["SearchQuery"] = searchQuery;

                offices = result.OfficeTypeOffices;

                return PartialView(offices);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("new_office_type")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Create()
        {
            return PartialView();
        }

        // POST: OfficeTypes/Create
        [Route("new_office_type")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfficeTypesCreateDto model)
        {
            try
            {
                var url = Url.Action("Create");

                if (ModelState.IsValid)
                {

                    //Check if entry with similar data already exists
                    if (await _officeTypesRepository.Exists(model.TypeDescription))
                    {

                        _flashMessage.Danger("Duplicate Data Entry!", "An Office Type with the same description already exists in the system...");
                        return Json(url);
                    }

                    var result = await _officeTypesRepository.Create(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "New Office Type Added Successfully!");
                        url = Url.Action("Index", "OfficeTypes");
                        return Json(url);
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


        // GET: Offices/Create
        [Route("new_office")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> CreateOffice(Guid id)
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
                 Selected = x.Id == id,
             }).ToList();
            //
            ViewData["States"] = states.OrderBy(x => x.StateCode).Select(x =>
             new SelectListItem()
             {
                 Text = $"{x.StateCode} {x.StateName}",
                 Value = x.Id.ToString(),
             }).ToList();

            var model = new OfficesCreateDto() { OfficeTypeId = id };

            return PartialView(model);
        }

        [Route("new_office")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOffice(OfficesCreateDto model)
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

                var url = Url.Action("CreateOffice", new { id = model.OfficeTypeId });

                if (ModelState.IsValid)
                {


                    //Check if entry with similar data already exists
                    if (await _officesRepository.Exists(model.OfficeName))
                    {

                        _flashMessage.Danger("Duplicate Data Entry!", "An Office with the same Name already exists in the system...");
                        return Json(url);
                    }
                    //model.Id = Guid.Empty;
                    var result = await _officesRepository.Create(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "New Office Added Successfully!");
                        url = Url.Action("ViewOffices", "OfficeTypes", new { id = model.OfficeTypeId });
                        return Json(url);
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

                var officeType = await _officeTypesRepository.Get(id.Value);
                var model = _mapper.Map<OfficeTypesCreateDto>(officeType);
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
        public async Task<IActionResult> Update(OfficeTypesCreateDto model)
        {

            try
            {
                var url = Url.Action("Update", new { id = model.Id });
                if (ModelState.IsValid)
                {


                    //Check if entry with similar data already exists
                    if (await _officeTypesRepository.Exists(model.Id, model.TypeDescription))
                    {

                        _flashMessage.Danger("Duplicate Data Entry!", "An Office Type with the same description already exists in the system...");
                        return Json(url);
                    }

                    //Set Pins as active 
                    var result = await _officeTypesRepository.Update(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "Office Type Updated Successfully!");
                        url = Url.Action("ViewPins", "OfficeTypes", new { id = model.Id });
                        return Json(url);
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

                var officeType = await _officeTypesRepository.Get(id.Value);

                if (officeType == null)
                {
                    return NotFound();
                }




                return PartialView(officeType);
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
        public async Task<IActionResult> Delete(OfficeTypesViewDto model)
        {
            try
            {
                var url = Url.Action("Delete", new { id = model.Id });
                if (ModelState.IsValid)
                {
                    await _officeTypesRepository.Delete(model.Id);

                    _flashMessage.Info("Delete Successful", "Office Type removed from system!");
                    url = Url.Action("Index", "OfficeTypes");
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