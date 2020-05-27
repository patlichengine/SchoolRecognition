using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("manage_pins/recognition_pins")]
    public class PinsController : Controller
    {
        private IFlashMessage _flashMessage;
        private IPinsRepository _pinsRepository;
        private IRecognitionTypesRepository _recognitionTypesRepository;
        private int _maximumNumberOfPinsToGenerate = 10;

        public PinsController(IFlashMessage flashMessage, IPinsRepository pinsRepository, IRecognitionTypesRepository recognitionTypesRepository)
        {
            _flashMessage = flashMessage;
            _pinsRepository = pinsRepository ??
               throw new ArgumentNullException(nameof(pinsRepository));
            _recognitionTypesRepository = recognitionTypesRepository ??
               throw new ArgumentNullException(nameof(pinsRepository));
        }

        [Route("")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Index(string orderBy, string searchQuery, int? pageNumber)
        {
            try
            {



                IEnumerable<int> pages = new List<int>();

                var resourceParams = new PinsResourceParams()
                {
                    PageNumber = pageNumber != null ? pageNumber.Value : 1,
                    SearchQuery = !String.IsNullOrWhiteSpace(searchQuery) ? searchQuery : null,
                    OrderBy = !String.IsNullOrWhiteSpace(orderBy) ? searchQuery : "DateCreated",
                };
                //Instantiate PagedList
                PagedList<PinsViewDto> pins = PagedList<PinsViewDto>
                         .Create(Enumerable.Empty<PinsViewDto>().AsQueryable(),
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
                    case "serial_number_desc":
                        resourceParams.OrderBy = "SerialNumberDesc";
                        break;
                    case "serial_number":
                        resourceParams.OrderBy = "SerialNumber";
                        break;
                    default:
                        resourceParams.OrderBy = "DateCreated";
                        break;
                }

                var result = await _pinsRepository.PagedList(resourceParams);

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

                pins = result;

                ViewData["Pages"] = pages;
                ViewData["OrderBy"] = orderBy;
                ViewData["SearchQuery"] = searchQuery;


                return PartialView(pins);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("pin_details/{id?}")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Details(Guid? id)
        {
            try
            {
                if (id == null ||  id.Value == Guid.Empty)
                {
                    return BadRequest();
                }
                var result = await _pinsRepository.GetIncludingListOfSchoolPayments(id.Value);
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
        
        [Route("pin_user_tracker/{id?}")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> PinHistories(Guid id, string orderBy, string searchQuery, int? pageNumber)
        {
            try
            {

                if (id == Guid.Empty)
                {
                    return BadRequest();
                }

                IEnumerable<int> pages = new List<int>();

                var resourceParams = new PinHistoriesResourceParams()
                {
                    PageNumber = pageNumber != null ? pageNumber.Value : 1,
                    SearchQuery = !String.IsNullOrWhiteSpace(searchQuery) ? searchQuery : null,
                    OrderBy = !String.IsNullOrWhiteSpace(orderBy) ? searchQuery : "DateActive",
                };
                //Instantiate PagedList
                PagedList<PinHistoriesViewDto> pins = PagedList<PinHistoriesViewDto>
                         .Create(Enumerable.Empty<PinHistoriesViewDto>().AsQueryable(),
                             resourceParams.PageNumber,
                             resourceParams.PageSize);
                switch (orderBy)
                {
                    case "date_desc":
                        resourceParams.OrderBy = "DateActiveDesc";
                        break;
                    case "date":
                        resourceParams.OrderBy = "DateActive";
                        break;
                    default:
                        resourceParams.OrderBy = "DateActive";
                        break;
                }

                var result = await _pinsRepository.GetIncludingPagedListOfPinHistories(id, resourceParams);

                if (result != null)
                {
                    var totalPages = result.Histories.TotalPages;

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
                ViewData["Pin"] = result;
                ViewData["OrderBy"] = orderBy;
                ViewData["SearchQuery"] = searchQuery;

                pins = result.Histories;

                return PartialView(pins);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [Route("generate_pins")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> GeneratePins()
        {
            try
            {

                #region PinCreationDependencys

                var pinsCreationDependencys = await _pinsRepository.GetCreationDepedencys();

                var recognitionTypes = pinsCreationDependencys.RecognitionTypes;
                var applicationSetting = pinsCreationDependencys.ApplicationSetting;

                ViewData["RecognitionTypes"] = recognitionTypes.OrderBy(x => x.RecognitionTypeName).Select(x =>
                 new SelectListItem()
                 {
                     Text = x.RecognitionTypeName,
                     Value = x.Id.ToString(),
                 }).ToList();

                if (applicationSetting != null && applicationSetting.MaximumNoOfPinsToGenerate != null && applicationSetting.MaximumNoOfPinsToGenerate > 0)
                {
                    _maximumNumberOfPinsToGenerate = applicationSetting.MaximumNoOfPinsToGenerate.Value;
                }
                ViewData["MaximumNumberOfPinsToGenerate"] = _maximumNumberOfPinsToGenerate;


                #endregion

                return PartialView();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [Route("generate_pins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GeneratePins(PinsCreateDto model)
        {

            try
            {

                #region PinCreationDependencys

                var pinsCreationDependencys = await _pinsRepository.GetCreationDepedencys();

                var recognitionTypes = pinsCreationDependencys.RecognitionTypes;
                var applicationSetting = pinsCreationDependencys.ApplicationSetting;

                ViewData["RecognitionTypes"] = recognitionTypes.OrderBy(x => x.RecognitionTypeName).Select(x =>
                 new SelectListItem()
                 {
                     Text = x.RecognitionTypeName,
                     Value = x.Id.ToString(),
                 }).ToList();

                if (applicationSetting != null && applicationSetting.MaximumNoOfPinsToGenerate != null && applicationSetting.MaximumNoOfPinsToGenerate > 0)
                {
                    _maximumNumberOfPinsToGenerate = applicationSetting.MaximumNoOfPinsToGenerate.Value;
                }
                ViewData["MaximumNumberOfPinsToGenerate"] = _maximumNumberOfPinsToGenerate;


                #endregion

                if (ModelState.IsValid)
                {
                    model.IsActive = true;
                    var result = await _pinsRepository.CreateMultiple(model);

                    if (result)
                    {
                        _flashMessage.Confirmation("Operation Completed", String.Format("{0} PINs generated", model.NoOfPinToGenerate));
                        return RedirectToAction("Index", "Pins");
                    }
                    else
                    {
                        _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly...");
                        return View(model);
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
        [Route("delete_pin/{id?}")]
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

                var recognitionType = await _pinsRepository.Get(id.Value);

                if (recognitionType == null)
                {
                    return NotFound();
                }




                return PartialView(recognitionType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: Offices/Delete/5
        [Route("delete_pin/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PinsViewDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _pinsRepository.Delete(model.Id);

                    _flashMessage.Info("Delete Successful", "Pin removed from system!");
                    return RedirectToAction("Index", "RecognitionTypes");
                }
                _flashMessage.Danger("Oops...Something went wrong!", "Invalid operation parameters!");
                return PartialView(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}