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

    [Route("manage_class_settings/home")]
    public class ClassSettings : Controller
    {

        private IFlashMessage _flashMessage;
        private IClassSettingsRepository _classSettings;
        private readonly IMapper _mapper;

        public ClassSettings(IFlashMessage flashMessage, IClassSettingsRepository classSettings, IMapper mapper)
        {
            _flashMessage = flashMessage ??
                throw new ArgumentNullException(nameof(mapper));
            _classSettings = classSettings ??
               throw new ArgumentNullException(nameof(classSettings));
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

                var resourceParams = new ClassSettingsResourceParams()
                {
                    PageNumber = pageNumber != null ? pageNumber.Value : 1,
                    SearchQuery = !String.IsNullOrWhiteSpace(searchQuery) ? searchQuery : null,
                    OrderBy = !String.IsNullOrWhiteSpace(orderBy) ? searchQuery : "Name",
                };
                //Instantiate PagedList
                PagedList<ClassSettingsDto> items = PagedList<ClassSettingsDto>
                         .Create(Enumerable.Empty<ClassSettingsDto>().AsQueryable(),
                             resourceParams.PageNumber,
                             resourceParams.PageSize);
                switch (orderBy)
                {
                    case "name_desc":
                        resourceParams.OrderBy = "Name";
                        break;
                    case "name_asc":
                        resourceParams.OrderBy = "Name";
                        break;
                    default:
                        resourceParams.OrderBy = "Name";
                        break;
                }

               


                var result = await _classSettings.List(resourceParams);

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
        public async Task<ActionResult<ClassSettingsDto>> Create(ClassSettingsCreateDto model)
        {
            
            if (ModelState.IsValid)
            {


                await _classSettings.Create(model);

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



                var result = await _classSettings.ListById(id);

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
            var model = await _classSettings.ListById(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
            
        }


        [Route("update/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id, ClassSettingsUpdateDto model)
        {

            try
            {

                await _classSettings.Update(id, model);
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