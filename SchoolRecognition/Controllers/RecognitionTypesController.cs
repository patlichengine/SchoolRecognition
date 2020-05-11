using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        public async Task<IActionResult> Index()
        {
            try
            {

                var recognitionTypes = await _recognitionTypesRepository.GetAllRecognitionTypesAsync();

                return View(recognitionTypes);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        //Get id
        [Route("recognition_type_pins/{id?}")]
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
                //Instantiate CustomPagedList
                CustomPagedList<PinsViewDto> pins = CustomPagedList<PinsViewDto>
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

                var result = await _recognitionTypesRepository.GetRecognitionTypesPinsAsPagedListAsync(id, resourceParams);

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

                return View(pins);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("create")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: RecognitionTypes/Create
        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecognitionTypesCreateDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _recognitionTypesRepository.CreateRecognitionTypeAsync(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "New Recognition Type Added Successfully!");
                        return RedirectToAction("Index", "RecognitionTypes");
                    }
                    else
                    {
                        _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly...");
                        return View(model);
                    }
                }
                _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly...");
                return View(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }




        //CreatePins
        [Route("generate_pins/{id?}")]
        [HttpGet]
        public async Task<IActionResult> GeneratePins(Guid id)
        {
            try
            {
                var recognitionTypes = await _recognitionTypesRepository.GetAllRecognitionTypesAsync();

                ViewData["RecognitionTypes"] = recognitionTypes.OrderBy(x => x.RecognitionTypeName).Select(x =>
                 new SelectListItem()
                 {
                     Text = x.RecognitionTypeName,
                     Value = x.Id.ToString(),
                     Selected = (x.Id == id)
                 }).ToList();

                var model = new PinsCreateDto() { RecognitionTypeId = id };

                return View(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [Route("generate_pins/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GeneratePins(PinsCreateDto model)
        {

            try
            {
                var recognitionTypes = await _recognitionTypesRepository.GetAllRecognitionTypesAsync();
                //
                ViewData["RecognitionTypes"] = recognitionTypes.OrderBy(x => x.RecognitionTypeName).Select(x =>
                 new SelectListItem()
                 {
                     Text = x.RecognitionTypeName,
                     Value = x.Id.ToString()
                 }).ToList();

                if (ModelState.IsValid)
                {
                    //Set Pins as active 
                    model.IsActive = true;
                    var result = await _pinsRepository.CreateMultiplePinAsync(model);

                    if (result)
                    {
                        _flashMessage.Confirmation("Operation Completed", String.Format("{0} PINs generated", model.NoOfPinToGenerate));
                        return RedirectToAction("ViewPins", "RecognitionTypes", new { id = model.RecognitionTypeId });
                    }
                    else
                    {
                        _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly...");
                        return View(model);
                    }
                }
                _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly...");
                return View(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [Route("update/{id?}")]
        [HttpGet]
        public async Task<IActionResult> Update(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var recognitionType = await _recognitionTypesRepository.GetRecognitionTypesSingleOrDefaultAsync(id.Value);
                var model = _mapper.Map<RecognitionTypesCreateDto>(recognitionType);
                return View(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: Offices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Route("update/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(RecognitionTypesCreateDto model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    //Set Pins as active 
                    var result = await _recognitionTypesRepository.UpdateRecognitionTypeAsync(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "Recognition Type Updated Successfully!");
                        return RedirectToAction("ViewPins", "RecognitionTypes", new { id = model.Id });
                    }
                    else
                    {
                        _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly...");
                        return View(model);
                    }
                }
                _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly...");
                return View(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: Offices/Delete/5
        [Route("delete/{id?}")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var recognitionType = await _recognitionTypesRepository.GetRecognitionTypesSingleOrDefaultAsync(id.Value);

                if (recognitionType == null)
                {
                    return NotFound();
                }




                return View(recognitionType);
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
                if (ModelState.IsValid)
                {
                    await _recognitionTypesRepository.DeleteRecognitionTypeAsync(model.Id);

                    _flashMessage.Info("Delete Successful", "Recognition Type removed from system!");
                    return RedirectToAction("Index", "RecognitionTypes");
                }
                _flashMessage.Danger("Oops...Something went wrong!", "Invalid operation parameters!");
                return View(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}