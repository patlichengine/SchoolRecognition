using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using SchoolRecognition.Services;

namespace SchoolRecognition.Controllers
{
    [Route("manage_facilities/types")]
    public class FacilityTypesController : Controller
    {
        private readonly IFacilityTypesRepository facilityTypes;

        public FacilityTypesController(IFacilityTypesRepository facilityTypesRepository)
        {
            facilityTypes = facilityTypesRepository;
        }

        // GET: Facility
        [Route("Home")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Index(string orderBy, string searchQuery, int? pageNumber)
        {
            try
            {

                //if (HttpContext.Session.Get<string>("UserId") != null)
                //{
                IEnumerable<int> pages = new List<int>();

                var resourceParams = new FacilityTypesResourceParams()
                {
                    PageNumber = pageNumber != null ? pageNumber.Value : 1,
                    SearchQuery = !String.IsNullOrWhiteSpace(searchQuery) ? searchQuery : null,
                    OrderBy = !String.IsNullOrWhiteSpace(orderBy) ? searchQuery : "Name",
                };
                //Instantiate CustomPagedList
                PagedList<FacilityTypesDto> objData = PagedList<FacilityTypesDto>
                         .Create(Enumerable.Empty<FacilityTypesDto>().AsQueryable(),
                             resourceParams.PageNumber,
                             resourceParams.PageSize);
                switch (orderBy)
                {
                    case "Title_Desc":
                        resourceParams.OrderBy = "Title";
                        break;
                    case "Title":
                        resourceParams.OrderBy = "Title";
                        break;


                    default:
                        resourceParams.OrderBy = "Title";
                        break;
                }

                var result = await facilityTypes.List(resourceParams);

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
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: Facility/Details/5
        [Route("type_detail")]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await facilityTypes.ListById(id);
            return View(model);
        }

        // GET: Facility/Create
        [Route("new")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Facility/Create
        [Route("new")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<FacilityTypesDto>> Create(CreateFacilityTypesDto createFacility)
        {
            if (ModelState.IsValid)
            {
                await facilityTypes.Create(createFacility);


                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: Facility/Edit/5
        [Route("manage_type/{id}")]
        public async Task<ActionResult<FacilityTypesDto>> Edit(Guid id)
        {
            var model = await facilityTypes.ListById(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Facility/Edit/5
        [Route("manage_type/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateFacilityTypesDto updateFacility)
        {
            try
            {
                // TODO: Add update logic here
                await facilityTypes.Update(id, updateFacility);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: Facility/Delete/5
        [Route("remove_type/{id}")]
        public ActionResult Delete(Guid id)
        {
            if (!facilityTypes.FacilityTypesExists(id).Result)
            {
                // return NotFound();
                return NotFound();
            }
            //var ind = schoolCategories.GetCategoryById(id);


            facilityTypes.Delete(id);

            // await  schoolCategories.Save();

            return RedirectToAction(nameof(Index));
        }

        // POST: Facility/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}