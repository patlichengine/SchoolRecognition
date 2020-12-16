using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using SchoolRecognition.Services;
using Vereyon.Web;

namespace SchoolRecognition.Controllers
{
    [Route("manage_subjects")]
    public class SubjectsController : Controller
    {

        private IFlashMessage _flashMessage;
        private ISubjectsRepository _subjectsRepository;
        private readonly IMapper _mapper;

        public SubjectsController(IFlashMessage flashMessage, ISubjectsRepository subjectsRepository, IMapper mapper)
        {
            _flashMessage = flashMessage ??
                throw new ArgumentNullException(nameof(mapper));
            _subjectsRepository = subjectsRepository ??
               throw new ArgumentNullException(nameof(subjectsRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        // GET: Subjects
        [Route("")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Index(string orderBy, string searchQuery, int? pageNumber)
        {
            try
            {



                IEnumerable<int> pages = new List<int>();

                var resourceParams = new SubjectsResourceParams()
                {
                    PageNumber = pageNumber != null ? pageNumber.Value : 1,
                    SearchQuery = !String.IsNullOrWhiteSpace(searchQuery) ? searchQuery : null,
                    OrderBy = !String.IsNullOrWhiteSpace(orderBy) ? searchQuery : "SubjectName",
                };
                //Instantiate PagedList
                PagedList<SubjectsViewDto> subjects = PagedList<SubjectsViewDto>
                         .Create(Enumerable.Empty<SubjectsViewDto>().AsQueryable(),
                             resourceParams.PageNumber,
                             resourceParams.PageSize);
                switch (orderBy)
                {
                    case "subject_name_desc":
                        resourceParams.OrderBy = "LongNameDesc";
                        break;
                    case "subject_name":
                        resourceParams.OrderBy = "LongName";
                        break;
                    default:
                        resourceParams.OrderBy = "LongName";
                        break;
                }

                var result = await _subjectsRepository.PagedList(resourceParams);

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

                subjects = result;

                ViewData["Pages"] = pages;
                ViewData["OrderBy"] = orderBy;
                ViewData["SearchQuery"] = searchQuery;


                return PartialView(subjects);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    
        // GET: Subjects/Create
        [Route("new_subject")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Create()
        {
            try
            {


                return PartialView();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("new_subject")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubjectsCreateDto model)
        {

            try
            {
                var url = Url.Action("Create");
                if (ModelState.IsValid)
                {


                    //Check if entry with similar data already exists
                    if (await _subjectsRepository.Exists(model.SubjectCode, model.LongName, model.ShortName))
                    {

                        _flashMessage.Danger("Duplicate Data Entry!", "An Subject with the same Name already exists in the system...");
                        return Json(url);
                    }

                    var result = await _subjectsRepository.Create(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "New Subject Added Successfully!");
                        url = Url.Action("Index", "Subjects");
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

        // GET: Subjects/Edit/5

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
                    return NotFound();
                }

                var subject = await _subjectsRepository.Get(id.Value);

                if (subject == null)
                {
                    return BadRequest();
                }
                var model = new SubjectsCreateDto()
                {
                    Id = subject.Id,
                    SubjectCode = subject.SubjectCode,
                    LongName = subject.LongName,
                    ShortName = subject.ShortName, 
                    HasItem = subject.HasItem,
                    IsTradeSubject = subject.IsTradeSubject,
                    IsCoreSubject = subject.IsCoreSubject
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
        public async Task<IActionResult> Update(SubjectsCreateDto model)
        {

            try
            {
                var url = Url.Action("Update", new { id = model.Id });
                if (ModelState.IsValid)
                {

                    //Check if entry with similar data already exists
                    if (await _subjectsRepository.Exists(model.Id, model.SubjectCode, model.LongName, model.ShortName))
                    {

                        _flashMessage.Danger("Duplicate Data Entry!", "An Subject with the same Name already exists in the system...");
                        return Json(url);
                    }

                    //Set Pins as active 
                    var result = await _subjectsRepository.Update(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "Subject Updated Successfully!");
                        url = Url.Action("Index", "Subjects");
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

        // GET: Subjects/Delete/5
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

                var subject = await _subjectsRepository.Get(id.Value);

                if (subject == null)
                {
                    return NotFound();
                }




                return PartialView(subject);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: Subjects/Delete/5
        [Route("delete/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(SubjectsViewDto model)
        {
            try
            {
                var url = Url.Action("Delete", new { id = model.Id });
                if (ModelState.IsValid)
                {
                    await _subjectsRepository.Delete(model.Id);

                    _flashMessage.Info("Delete Successful", "Subject removed from system!");
                    url = Url.Action("Index", "Subjects");
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