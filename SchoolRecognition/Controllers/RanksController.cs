using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRecognition.Extensions;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using SchoolRecognition.Services;
using static SchoolRecognition.Extensions.CustomViewHelper;

namespace SchoolRecognition.Controllers
{
    [Route("Ranks")]
    public class RanksController : Controller
    {
        private readonly IRanksRepository _ranks;
       

        public RanksController(IRanksRepository ranks)
        {
            _ranks = ranks;
           
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

                var resourceParams = new RanksResourceParams()
                {
                    PageNumber = pageNumber != null ? pageNumber.Value : 1,
                    SearchQuery = !String.IsNullOrWhiteSpace(searchQuery) ? searchQuery : null,
                    OrderBy = !String.IsNullOrWhiteSpace(orderBy) ? searchQuery : "Name",
                };
                //Instantiate CustomPagedList
                PagedList<RanksDto> objData = PagedList<RanksDto>
                         .Create(Enumerable.Empty<RanksDto>().AsQueryable(),
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

                var result = await _ranks.List(resourceParams);

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



        [Route("rank_information")]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _ranks.ListById(id);
            return View(model);
        }

       
        [Route("new")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("new")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<RanksDto>> Create(CreateRanksDto model)
        {
            if (ModelState.IsValid)
            {
                await _ranks.Create(model);


                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [Route("manage_rank/{id}")]
        public async Task<ActionResult<RanksDto>> Edit(Guid id)
        {


            var model = await _ranks.ListById(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }


        [Route("manage_rank/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateRanksDto ranks)

        {
            try
            {
                await _ranks.Update(id, ranks);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            return View();


        }

        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!_ranks.RanksExists(id).Result)
            {
                return NotFound();
            }

            await _ranks.Delete(id);

            return RedirectToAction(nameof(Index));
        }


    }
}