using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Hosting.
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NToastNotify;
using SchoolRecognition.Entities;
using SchoolRecognition.Extensions;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using SchoolRecognition.Services;
using Vereyon.Web;


namespace SchoolRecognition.Controllers
{
    [Route("manage_schools/SchoolCategory")]
    public class SchoolCategoriesController : Controller
    {


        private readonly IToastNotification _toastNotification;
        private readonly ISchoolCategoryRepository schoolCategories;
        private IFlashMessage _flashMessage;

        private readonly ILogger _logger;


        public SchoolCategoriesController(ISchoolCategoryRepository schoolCategory, IFlashMessage flashMessage, ILogger<SchoolCategoriesController> logger, IToastNotification toastNotification)
        {

            _logger = logger;

            schoolCategories = schoolCategory ?? throw new ArgumentNullException(nameof(schoolCategories));

            _flashMessage = flashMessage ?? throw new ArgumentNullException(nameof(flashMessage));

            _toastNotification = toastNotification ?? throw new ArgumentNullException(nameof(toastNotification));


        }

        [Route("")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Index(string orderBy, string searchQuery, int? pageNumber)
        {
            try
            {

                //if (HttpContext.Session.Get<string>("UserId") != null)
                //{
                IEnumerable<int> pages = new List<int>();

                var resourceParams = new SchoolCategoriesResourceParams()
                {
                    PageNumber = pageNumber != null ? pageNumber.Value : 1,
                    SearchQuery = !String.IsNullOrWhiteSpace(searchQuery) ? searchQuery : null,
                    OrderBy = !String.IsNullOrWhiteSpace(orderBy) ? searchQuery : "Name",
                };
                //Instantiate CustomPagedList
                PagedList<SchoolCategoryDto> objData = PagedList<SchoolCategoryDto>
                         .Create(Enumerable.Empty<SchoolCategoryDto>().AsQueryable(),
                             resourceParams.PageNumber,
                             resourceParams.PageSize);
                switch (orderBy)
                {
                    case "Name_Desc":
                        resourceParams.OrderBy = "Name";
                        break;
                    case "Name":
                        resourceParams.OrderBy = "Name";
                        break;
                    case "Code_Desc":
                        resourceParams.OrderBy = "Code";
                        break;
                    case "Code":
                        resourceParams.OrderBy = "Code";
                        break;

                    default:
                        resourceParams.OrderBy = "Name";
                        break;
                }

                var result = await schoolCategories.List(resourceParams);

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

                objData = result;

                ViewData["Pages"] = pages;
                ViewData["OrderBy"] = orderBy == null ? "" : orderBy;
                ViewData["SearchQuery"] = searchQuery == null ? "" : searchQuery;


                return PartialView(objData);
                //} else
                //{
                //    return StatusCode(StatusCodes.Status403Forbidden);
                //}
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        //[Route("Available_categories")]
        //public async Task<ActionResult<SchoolCategoryDto>> Index()
        //{
        //    var result = await schoolCategories.List();

        //    return View(result);
        //}

        //get all centres attached to a category
        [Route("available_centres/centres/{centId}")]
        public async Task<ActionResult<CentresViewDto>> ListAllCentres(Guid centId)
        {
            var result = await schoolCategories.ListCentresForACategory(centId);
            return View(result);
        }

        //get all schools attached to a category
        [Route("available_schools/schools/{catID}")]
        public async Task<ActionResult<SchoolsViewDto>> ListAllSchools(Guid catID)
        {
            var result = await schoolCategories.ListSchoolsForACategory(catID);
            return View(result);
        }

        //get a centre of a category
        [Route("manage_category/centre/{centId}")]
        public async Task<ActionResult<SchoolCategoryDto>> ListCenter(Guid centId)
        {
            if (centId == null)
            {
                return NotFound();
            }

            var centre = await schoolCategories.ListCategoryForACentreById(centId);
            if (centre == null)
            {
                return NotFound();
            }
            return View(centre);
        }

        //get a school of a category
        [Route("manage_category/school/{schId}")]
        public async Task<ActionResult<SchoolCategoryDto>> ListSchool(Guid schId)
        {
            if (schId == null)
            {
                return NotFound();
            }

            var school = await schoolCategories.ListCategoryForASchoolById(schId);
            if (school == null)
            {
                return NotFound();
            }
            return View(school);
        }

        [Route("new")]
        public IActionResult Create()
        {
            return View();
        }
        [Route("new")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<SchoolCategoryDto>> Create(CreateSchoolCategoryDto model)
        {
            if (ModelState.IsValid)
            {
                await schoolCategories.Create(model);
                _toastNotification.AddSuccessToastMessage("New Category Added Successfully!");
                // _flashMessage.Confirmation("New Category Added Successfully! As: ", model.Name);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        //[HttpGet("{id}")]
        [Route("manage_category/{id}")]
        public async Task<ActionResult<SchoolCategoryDto>> Edit(Guid id)
        {


            _logger.LogInformation("Getting item {Id} at {RequestTime}", id, DateTime.Now);
            var model = await schoolCategories.ListById(id);
            // var model = await _context.SchoolCategories.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Route("manage_category/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateSchoolCategoryDto school)

        {
            try
            {
                await schoolCategories.Update(id, school);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            return View();


        }


        // GET: SchoolCategory/Details/5


        [Route("category_details/{id?}")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Details(Guid? id)
        {
            try
            {
                if (id == null || id.Value == Guid.Empty)
                {
                    return BadRequest();
                }
                var result = await schoolCategories.ListById(id.Value);
                if (result == null)
                {
                    return NotFound();
                }
                return PartialView(result);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}