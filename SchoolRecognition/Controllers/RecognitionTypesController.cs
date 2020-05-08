using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolRecognition.Models;
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

        public RecognitionTypesController(IFlashMessage flashMessage, IPinsRepository pinsRepository, IRecognitionTypesRepository recognitionTypesRepository)
        {
            _flashMessage = flashMessage;
            _pinsRepository = pinsRepository ??
               throw new ArgumentNullException(nameof(pinsRepository));
            _recognitionTypesRepository = recognitionTypesRepository ??
               throw new ArgumentNullException(nameof(pinsRepository));
        }


        //Get
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var recognitionTypes = await _recognitionTypesRepository.GetAllRecognitionTypesAsync();

            return View(recognitionTypes);
        }
        //Get id
        [Route("view_all_pins")]
        public async Task<IActionResult> ViewPins(Guid id, string sortOrder, string searchQuery, int? pageNumber)
        {

            return View();
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
            if (ModelState.IsValid)
            {
                var result = await _recognitionTypesRepository.CreateRecognitionTypeAsync(model);

                if (result != null)
                {
                    _flashMessage.Confirmation("Operation Completed", "New Recognition Type Added Successfully!");
                    return RedirectToAction("Index", "RecognitionTypes" );
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


        //CreatePins
        [Route("generate_pins")]
        [HttpGet]
        public async Task<IActionResult> GeneratePins(Guid id)
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


        [Route("generate_pins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GeneratePins(PinsCreateDto model)
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
    }
}