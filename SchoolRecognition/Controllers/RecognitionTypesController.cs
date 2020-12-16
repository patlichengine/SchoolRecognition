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
    [Route("manage_pins")]
    public class RecognitionTypesController : Controller
    {
        private IFlashMessage _flashMessage;
        private IPinsRepository _pinsRepository;
        private IRecognitionTypesRepository _recognitionTypesRepository;
        private readonly IMapper _mapper;
        private int _maximumNumberOfPinsToGenerate = 10;

        public RecognitionTypesController(IFlashMessage flashMessage, IPinsRepository pinsRepository, IRecognitionTypesRepository recognitionTypesRepository, IMapper mapper)
        {
            _flashMessage = flashMessage ??
                throw new ArgumentNullException(nameof(mapper));
            _pinsRepository = pinsRepository ??
               throw new ArgumentNullException(nameof(pinsRepository));
            _recognitionTypesRepository = recognitionTypesRepository ??
               throw new ArgumentNullException(nameof(pinsRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }


        //Get
        [Route("")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Index()
        {
            try
            {

                var recognitionTypes = await _recognitionTypesRepository.List();

                return PartialView(recognitionTypes);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        //Get id
        [Route("recognition_type/{id:guid}/pins")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> ViewPins(Guid id, string orderBy, string searchQuery, int? pageNumber)
        {
            try
            {

                if (id == Guid.Empty)
                {
                    return BadRequest();
                }



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

                var result = await _recognitionTypesRepository.GetIncludingPagedListOfPins(id, resourceParams);

                if (result != null)
                {
                    var totalPages = result.RecognitionTypePins.TotalPages;

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
                ViewData["RecognitionType"] = result;
                ViewData["OrderBy"] = orderBy;
                ViewData["SearchQuery"] = searchQuery;

                pins = result.RecognitionTypePins;

                return PartialView(pins);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("create")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Create()
        {
            return PartialView();
        }

        // POST: RecognitionTypes/Create
        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecognitionTypesCreateDto model)
        {
            try
            {
                var url = Url.Action("Create");
                if (ModelState.IsValid)
                {

                    //Check if entry with similar data already exists
                    if (await _recognitionTypesRepository.Exists(model.RecognitionTypeCode, model.RecognitionTypeName))
                    {

                        _flashMessage.Danger("Duplicate Data Entry!", "A Recognition Type with the same values already exists in the system...");
                        return Json(url);
                    }
                    var result = await _recognitionTypesRepository.Create(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "New Recognition Type Added Successfully!");
                        url = Url.Action("Index", "RecognitionTypes");
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




        //CreatePins
        [Route("generate_pins")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> GeneratePins(Guid id)
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

                var model = new PinsCreateDto() { RecognitionTypeId = id };

                return PartialView(model);
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

                ViewData["RecognitionTypes"] = recognitionTypes.OrderBy(x => x.RecognitionTypeName).Select(x =>
                 new SelectListItem()
                 {
                     Text = x.RecognitionTypeName,
                     Value = x.Id.ToString(),
                 }).ToList();

                ViewData["ApplicationSetting"] = applicationSetting;

                var url = Url.Action("GeneratePins", new { id = model.RecognitionTypeId });
                if (ModelState.IsValid)
                {
                    //Set Pins as active 
                    model.IsActive = true;
                    var result = await _pinsRepository.CreateMultiple(model);

                    if (result)
                    {
                        _flashMessage.Confirmation("Operation Completed", String.Format("{0} PINs generated", model.NoOfPinToGenerate));
                        url = Url.Action("ViewPins", "RecognitionTypes", new { id = model.RecognitionTypeId });
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

                var recognitionType = await _recognitionTypesRepository.Get(id.Value);
                var model = _mapper.Map<RecognitionTypesCreateDto>(recognitionType);
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
        public async Task<IActionResult> Update(RecognitionTypesCreateDto model)
        {

            try
            {
                var url = Url.Action("Update", new { id = model.Id });
                if (ModelState.IsValid)
                {
                    //Check if entry with similar data already exists
                    if (await _recognitionTypesRepository.Exists(model.Id, model.RecognitionTypeCode, model.RecognitionTypeName))
                    {
                        _flashMessage.Danger("Duplicate Data Entry!", "A Recognition Type with the same values already exists in the system...");
                        return Json(url);
                    }
                    //Set Pins as active 
                    var result = await _recognitionTypesRepository.Update(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "Recognition Type Updated Successfully!");
                        url = Url.Action("ViewPins", "RecognitionTypes", new { id = model.Id });
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

                var recognitionType = await _recognitionTypesRepository.Get(id.Value);

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
        [Route("delete/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RecognitionTypesViewDto model)
        {
            try
            {
                var url = Url.Action("Delete", new { id = model.Id });
                if (ModelState.IsValid)
                {
                    await _recognitionTypesRepository.Delete(model.Id);

                    _flashMessage.Info("Delete Successful", "Recognition Type removed from system!");
                    url = Url.Action("Index", "RecognitionTypes");
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