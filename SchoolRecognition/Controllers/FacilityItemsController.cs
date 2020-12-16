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

    [Route("manage_facility_items/")]
    public class FacilityItems : Controller
    {

        private IFlashMessage _flashMessage;
        private IFacilityItemsRepository _facilityItems;
        private readonly IMapper _mapper;

        public FacilityItems(IFlashMessage flashMessage, IFacilityItemsRepository facilityItems, IMapper mapper)
        {
            _flashMessage = flashMessage ??
                throw new ArgumentNullException(nameof(mapper));
            _facilityItems = facilityItems ??
               throw new ArgumentNullException(nameof(facilityItems));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        // GET: Schools
        [Route("home")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Index(string orderBy, string searchQuery, int? pageNumber)
        {
            try
            {



                IEnumerable<int> pages = new List<int>();

                var resourceParams = new FacilityItemsResourceParams()
                {
                    PageNumber = pageNumber != null ? pageNumber.Value : 1,
                    SearchQuery = !String.IsNullOrWhiteSpace(searchQuery) ? searchQuery : null,
                    OrderBy = !String.IsNullOrWhiteSpace(orderBy) ? searchQuery : "Description",
                };
                //Instantiate PagedList
                PagedList<FacilityItemsDto> items = PagedList<FacilityItemsDto>
                         .Create(Enumerable.Empty<FacilityItemsDto>().AsQueryable(),
                             resourceParams.PageNumber,
                             resourceParams.PageSize);
                switch (orderBy)
                {
                    case "description_desc":
                        resourceParams.OrderBy = "Description";
                        break;
                    case "description_asc":
                        resourceParams.OrderBy = "Description";
                        break;
                    default:
                        resourceParams.OrderBy = "Description";
                        break;
                }

               


                var result = await _facilityItems.List(resourceParams);

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

                items = result;

                ViewData["Pages"] = pages;
                ViewData["OrderBy"] = orderBy == null ? "" : orderBy;
               // ViewData["OrderBy"] = orderBy == null ? "" : orderBy;
                ViewData["SearchQuery"] = searchQuery == null ? "" : searchQuery;
              //  ViewData["SearchQuery"] = searchQuery == null ? "" : searchQuery;


                return PartialView(items);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Route("new")]
        public IActionResult Create()
        {
            return View();
        }
        [Route("new")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<FacilityItemsDto>> Create(FacilityItemsCreateDto model)
        {
            
            if (ModelState.IsValid)
            {


                await _facilityItems.Create(model);
                // _flashMessage.Confirmation("New Category Added Successfully! As: ", model.Name);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [Route("details/{id:guid}")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        // GET: Schools/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {

                if (id == Guid.Empty)
                {
                    return BadRequest();
                }



                var result = await _facilityItems.ListById(id);

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

     

    
        // GET: Schools/Edit/5

        [Route("update/{id?}")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Update(Guid id)
        {
            var model = await _facilityItems.ListById(id);
            // var model = await _context.SchoolCategories.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
            
        }


        [Route("update/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id, FacilityItemsUpdateDto model)
        {

            try
            {

                await _facilityItems.Update(id, model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            return View();
            
        }


    }
}