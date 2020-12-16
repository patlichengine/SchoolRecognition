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
    [Route("manage_facilities/settings")]
    public class FacilitySettingsController : Controller
    {
        private readonly IFacilitySettingsRepository facilitySettings;

        public FacilitySettingsController(IFacilitySettingsRepository facilitySettingsRepository)
        {
            facilitySettings = facilitySettingsRepository;
        }

        // GET: School
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

                var resourceParams = new FacilitySettingsResourceParams()
                {
                    PageNumber = pageNumber != null ? pageNumber.Value : 1,
                    SearchQuery = !String.IsNullOrWhiteSpace(searchQuery) ? searchQuery : null,
                    OrderBy = !String.IsNullOrWhiteSpace(orderBy) ? searchQuery : "Name",
                };
                //Instantiate CustomPagedList
                PagedList<FacilitySettingsDto> objData = PagedList<FacilitySettingsDto>
                         .Create(Enumerable.Empty<FacilitySettingsDto>().AsQueryable(),
                             resourceParams.PageNumber,
                             resourceParams.PageSize);
                switch (orderBy)
                {
                    case "Specification_Desc":
                        resourceParams.OrderBy = "Specification";
                        break;
                    case "Specification":
                        resourceParams.OrderBy = "Specification";
                        break;
                    case "Description_Desc":
                        resourceParams.OrderBy = "Description";
                        break;
                    case "Description":
                        resourceParams.OrderBy = "Description";
                        break;

                    default:
                        resourceParams.OrderBy = "Specification";
                        break;
                }

                var result = await facilitySettings.List(resourceParams);

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
                throw;
            }
        }



        // GET: School/Create
        [Route("new")]
        public async Task <IActionResult> Create()
        {
            // Get Category List
            List<FacilityItemsDto> facilityItemList = new List<FacilityItemsDto>();

            var facItems = await facilitySettings.GetAllFacilityItems();
            facilityItemList = (from c in facItems select c).ToList();

            ViewBag.facilityItemList = facilityItemList;

            // Get Category List
            List<FacilityTypesDto> facTypeList = new List<FacilityTypesDto>();

            var facTypes = await facilitySettings.GetAllFacilityTypes();
            facTypeList = (from c in facTypes select c).ToList();

            ViewBag.facilityList = facTypeList;

            //fetch Subject List
            List<SubjectsViewDto> subjectList = new List<SubjectsViewDto>();

            var subjects = await facilitySettings.GetAllSubjects();
            subjectList = (from s in subjects select s).ToList();

            ViewBag.subjectCode = subjectList;


            return View();
        }

        // POST: School/Create
        [Route("new")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<FacilitySettingsDto>> Create(CreateFacilitySettingsDto createFacSettingsDto)
        {
            try
            {
                // TODO: Add insert logic here
                await facilitySettings.Create(createFacSettingsDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
          
        }

        // GET: School/Edit/5
        [Route("manage/{id}")]
        public async Task<ActionResult<FacilitySettingsDto>> Edit(Guid id)
        {
            // Get facItemList List
            List<FacilityItemsDto> facItemList = new List<FacilityItemsDto>();

            var facItemSet = await facilitySettings.GetAllFacilityItems();
            facItemList = (from c in facItemSet select c).ToList();

            ViewBag.facItemList = facItemList;

            // Get Facility Type List
            List<FacilityTypesDto> facTypeList = new List<FacilityTypesDto>();

            var facTypes = await facilitySettings.GetAllFacilityTypes();
            facTypeList = (from c in facTypes select c).ToList();

            ViewBag.facList = facTypeList;

           
            //fetch Subject List
            List<SubjectsViewDto> subjectList = new List<SubjectsViewDto>();

            var subjects = await facilitySettings.GetAllSubjects();
            subjectList = (from s in subjects select s).ToList();

            ViewBag.subjectCode = subjectList;


            var model = await facilitySettings.ListById(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: School/Edit/5
        [Route("manage/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateFacilitySettingsDto  updateFacSettings)
        {
            try
            {
                // TODO: Add update logic here
                await facilitySettings.Update(id, updateFacSettings);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: School/Delete/5
        [Route("remove/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!facilitySettings.FacilitySettingsExists(id).Result)
            {
                // return NotFound();
                return NotFound();
            }
           await facilitySettings.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: School/Delete/5
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

        [Route("settings_details/{id?}")]
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
                var result = await facilitySettings.ListById(id.Value);
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