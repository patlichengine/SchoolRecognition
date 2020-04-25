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
    public class ManagePinsController : Controller
    {
        private IFlashMessage _flashMessage;
        private IPinsRepository _pinsService;
        private IRecognitionTypesRepository _recognitionTypesService;

        public ManagePinsController(IFlashMessage flashMessage, IPinsRepository pinsService, IRecognitionTypesRepository recognitionTypesService)
        {
            _flashMessage = flashMessage;
            _pinsService = pinsService;
            _recognitionTypesService = recognitionTypesService;
        }

        //Get
        public async Task<IActionResult> Index()
        {
            var recognitionTypes = await _recognitionTypesService.Get();

            return View(recognitionTypes);
        }
        //Get id
        public async Task<IActionResult> ViewPins(Guid id)
        {
            if (id != null && id != Guid.Empty)
            {
                List<PinsViewDto> pins = new List<PinsViewDto>();
                var recognitionType = await _recognitionTypesService.Get(id);
                if (recognitionType != null)
                {
                    ViewData["RecognitionType"] = recognitionType;
                    pins = recognitionType.RecognitionTypePins.ToList();
                    return View(pins);
                }
                else
                {
                    _flashMessage.Info("The requested resource could not be found!");
                    return RedirectToAction("Index", "RecognitionTypes");
                }
            }
            _flashMessage.Info("The requested resource could not be found!");
            return RedirectToAction("Index", "RecognitionTypes");
        }
        //Create
        public async Task<IActionResult> GeneratePins(Guid id)
        {
            var recognitionTypes = await _recognitionTypesService.Get();

            ViewData["RecognitionTypes"] = recognitionTypes.Select(x =>
           new SelectListItem() { 
               Text = x.RecognitionTypeName, 
               Value = x.Id.ToString(),
               Selected = (x.Id == id)
           }).ToList();

            var model = new PinsCreateDto() { RecognitionTypeId = id };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GeneratePins(PinsCreateDto model)
        {

            var recognitionTypes = await _recognitionTypesService.Get();
            //
            ViewData["RecognitionTypes"] = recognitionTypes.Select(x =>
           new SelectListItem() { Text = x.RecognitionTypeName, Value = x.Id.ToString() }).ToList();

            if (ModelState.IsValid)
            {
                //Set Pins as active 
                model.IsActive = true;
                var result = await _pinsService.CreateSeveralPins(model);

                if (result)
                {
                    _flashMessage.Confirmation("Operation Completed", String.Format("{0} PINs generated", model.NoOfPinToGenerate));
                    return RedirectToAction("ViewPins", "ManagePins", new { id = model.RecognitionTypeId });
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