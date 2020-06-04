using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using SchoolRecognition.Services;
using Vereyon.Web;

namespace SchoolRecognition.Controllers
{

    [Route("manage_schools")]
    public class SchoolsController : Controller
    {

        private IFlashMessage _flashMessage;
        private ISchoolsRepository _schoolsRepository;
        private ISchoolCategoryRepository _schoolCategorysRepository;
        private readonly IMapper _mapper;

        public SchoolsController(IFlashMessage flashMessage, ISchoolsRepository schoolsRepository, ISchoolCategoryRepository schoolCategorysRepository, IMapper mapper)
        {
            _flashMessage = flashMessage ??
                throw new ArgumentNullException(nameof(mapper));
            _schoolsRepository = schoolsRepository ??
               throw new ArgumentNullException(nameof(schoolsRepository));
            _schoolCategorysRepository = schoolCategorysRepository ??
               throw new ArgumentNullException(nameof(schoolsRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        // GET: Schools
        [Route("")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Index(string orderBy, string searchQuery, int? pageNumber)
        {
            try
            {



                IEnumerable<int> pages = new List<int>();

                var resourceParams = new SchoolsResourceParams()
                {
                    PageNumber = pageNumber != null ? pageNumber.Value : 1,
                    SearchQuery = !String.IsNullOrWhiteSpace(searchQuery) ? searchQuery : null,
                    OrderBy = !String.IsNullOrWhiteSpace(orderBy) ? searchQuery : "SchoolName",
                };
                //Instantiate PagedList
                PagedList<SchoolsViewDto> schools = PagedList<SchoolsViewDto>
                         .Create(Enumerable.Empty<SchoolsViewDto>().AsQueryable(),
                             resourceParams.PageNumber,
                             resourceParams.PageSize);
                switch (orderBy)
                {
                    case "year_est_desc":
                        resourceParams.OrderBy = "YearEstablishedDesc";
                        break;
                    case "year_est":
                        resourceParams.OrderBy = "YearEstablished";
                        break;
                    case "school_name_desc":
                        resourceParams.OrderBy = "SchoolNameDesc";
                        break;
                    case "school_name":
                        resourceParams.OrderBy = "SchoolName";
                        break;
                    default:
                        resourceParams.OrderBy = "SchoolName";
                        break;
                }

                var result = await _schoolsRepository.PagedList(resourceParams);

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

                schools = result;

                ViewData["Pages"] = pages;
                ViewData["OrderBy"] = orderBy;
                ViewData["SearchQuery"] = searchQuery;


                return PartialView(schools);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [Route("details/{id:guid}")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        // GET: Schools/Details/5
        public async Task<IActionResult> Details(Guid id, string orderBy, string searchQuery, int? pageNumber)
        {
            try
            {

                if (id == Guid.Empty)
                {
                    return BadRequest();
                }



                IEnumerable<int> pages = new List<int>();

                var resourceParams = new SchoolFacilitiesResourceParams()
                {
                    PageNumber = pageNumber != null ? pageNumber.Value : 1,
                    SearchQuery = !String.IsNullOrWhiteSpace(searchQuery) ? searchQuery : null,
                    OrderBy = !String.IsNullOrWhiteSpace(orderBy) ? searchQuery : "DateCreated",
                };
                //Instantiate PagedList
                PagedList<SchoolFacilitiesViewDto> schoolFacilitys = PagedList<SchoolFacilitiesViewDto>
                         .Create(Enumerable.Empty<SchoolFacilitiesViewDto>().AsQueryable(),
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
                    default:
                        resourceParams.OrderBy = "DateCreated";
                        break;
                }

                var result = await _schoolsRepository.GetIncludingPagedListOfSchoolFacilities(id, resourceParams);

                if (result != null)
                {
                    var totalPages = result.Facilities.TotalPages;

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
                ViewData["School"] = result;
                ViewData["OrderBy"] = orderBy;
                ViewData["SearchQuery"] = searchQuery;

                schoolFacilitys = result.Facilities;

                return PartialView(schoolFacilitys);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        // GET: Schools/Edit/5

        [Route("update/{id?}")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Update(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                #region SelectLists

                var creationDependencys = await _schoolsRepository.GetCreationDependencys();


                var schoolCategorys = creationDependencys.SchoolCategorys;
                var officeLocalGovernments = creationDependencys.OfficeLocalGovernments;

                //
                ViewBag.SchoolCategorys = schoolCategorys.OrderBy(x => x.Name).Select(x =>
                 new SelectListItem()
                 {
                     Text = x.Name,
                     Value = x.Id.ToString(),
                 }).ToList();


                //
                ViewBag.OfficeLocalGovernments = officeLocalGovernments.OrderBy(x => x.LocalGovernmentCode).Select(x =>
                 new SelectListItem()
                 {
                     Text = $"{x.LocalGovernmentCode} {x.LocalGovernmentName} [ {x.StateCode} {x.StateName} ]",
                     Value = x.LocalGovernmentId.ToString(),
                 }).ToList();

                #endregion


                if (id == null)
                {
                    return NotFound();
                }

                var school = await _schoolsRepository.Get(id.Value);

                if (school == null)
                {
                    return BadRequest();
                }
                var model = new SchoolsCreateDto()
                {
                    Id = school.Id,
                    SchoolName = school.SchoolName,
                    Address = school.Address,
                    EmailAddress = school.EmailAddress,
                    PhoneNo = school.PhoneNo,
                    YearEstablished = school.YearEstablished,
                    IsRecognised = school.IsRecognised,
                    IsVetted = school.IsVetted,
                    IsInspected = school.IsInspected,
                    IsCompleted = school.IsCompleted,
                    IsRecommended = school.IsRecommended,
                    HasDeficientSubject = school.HasDeficientSubject,
                    HasDeficientFacilitiy = school.HasDeficientFacilitiy,
                    //
                    CategoryId = school.CategoryId,
                    OfficeId = school.OfficeId,
                    LgId = school.LgId
                };

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
        public async Task<IActionResult> Update(SchoolsCreateDto model)
        {

            try
            {

                #region SelectLists

                var creationDependencys = await _schoolsRepository.GetCreationDependencys();


                var schoolCategorys = creationDependencys.SchoolCategorys;
                var officeLocalGovernments = creationDependencys.OfficeLocalGovernments;

                //
                ViewBag.SchoolCategorys = schoolCategorys.OrderBy(x => x.Name).Select(x =>
                 new SelectListItem()
                 {
                     Text = x.Name,
                     Value = x.Id.ToString(),
                 }).ToList();


                //
                ViewBag.OfficeLocalGovernments = officeLocalGovernments.OrderBy(x => x.LocalGovernmentCode).Select(x =>
                 new SelectListItem()
                 {
                     Text = $"{x.LocalGovernmentCode} {x.LocalGovernmentName} [ {x.StateCode} {x.StateName} ]",
                     Value = x.LocalGovernmentId.ToString(),
                 }).ToList();

                #endregion


                var url = Url.Action("Update", new { id = model.Id });

                if (ModelState.IsValid)
                {

                    //Check if entry with similar data already exists
                    if (await _schoolsRepository.Exists(model.Id, model.SchoolName))
                    {

                        _flashMessage.Danger("Duplicate Data Entry!", "An School with the same Name already exists in the system...");
                        return Json(url);
                    }

                    //Set Pins as active 
                    var result = await _schoolsRepository.Update(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "School Updated Successfully!");
                        url = Url.Action("Details", "Schools", new { id = model.Id });
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


    }
}