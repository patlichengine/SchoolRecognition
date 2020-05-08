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
    public class PinsController : Controller
    {
        private IFlashMessage _flashMessage;
        private IPinsRepository _pinsRepository;
        private IRecognitionTypesRepository _recognitionTypesRepository;

        public PinsController(IFlashMessage flashMessage, IPinsRepository pinsRepository, IRecognitionTypesRepository recognitionTypesRepository)
        {
            _flashMessage = flashMessage;
            _pinsRepository = pinsRepository ??
               throw new ArgumentNullException(nameof(pinsRepository));
            _recognitionTypesRepository = recognitionTypesRepository ??
               throw new ArgumentNullException(nameof(pinsRepository));
        }

        public async Task<IActionResult> Index(string sortOrder, string searchQuery, int? pageNumber)
        {
            List<PinsViewDto> pins = new List<PinsViewDto>();

            //Setting page number
            //var dbResult = await _c

            //Handling search queries
            //
            return View(pins);
        }
        public async Task<IActionResult> Details(Guid? id)
        {
            return View();
        }

        public async Task<IActionResult> GeneratePins()
        {
            var recognitionTypes = await _recognitionTypesRepository.GetAllRecognitionTypesAsync();

            ViewData["RecognitionTypes"] = recognitionTypes.OrderBy(x => x.RecognitionTypeName).Select(x =>
             new SelectListItem()
             {
                 Text = x.RecognitionTypeName,
                 Value = x.Id.ToString(),
             }).ToList();

            return View();
        }

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
                 Value = x.Id.ToString(),
             }).ToList();

            if (ModelState.IsValid)
            {
                model.IsActive = true;
                var result = await _pinsRepository.CreateMultiplePinAsync(model);

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
            return View(model);
        }
    }
}