using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolRecognition.Models;
using SchoolRecognition.Repository;
using Vereyon.Web;

namespace SchoolRecognition.Controllers
{
    public class PinsController : Controller
    {
        private IFlashMessage _flashMessage;
        private IPins _pinsService;
        private IRecognitionTypes _recognitionTypesService;

        public PinsController(IFlashMessage flashMessage, IPins pinsService, IRecognitionTypes recognitionTypesService)
        {
            _flashMessage = flashMessage;
            _pinsService = pinsService;
            _recognitionTypesService = recognitionTypesService;
        }

        public async Task<IActionResult> Index()
        {
            var pins = await _pinsService.Get();
            return View(pins);
        }
        public async Task<IActionResult> Details(Guid? id)
        {
            return View();
        }

        public async Task<IActionResult> GeneratePins()
        {
            var recognitionTypes = await _recognitionTypesService.Get();

            ViewData["RecognitionTypes"] =  recognitionTypes.Select(x => 
            new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GeneratePins(PinsCreateViewModel model)
        {

            var recognitionTypes = await _recognitionTypesService.Get();
            //
            ViewData["RecognitionTypes"] = recognitionTypes.Select(x =>
           new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();

            if (ModelState.IsValid)
            {
                var pinObject = new Pins() { RecognitionTypeId = model.RecognitionTypeId, IsActive = true };
                var result = await _pinsService.CreateSeveralPins(pinObject, model.NoOfPinToGenerate);

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