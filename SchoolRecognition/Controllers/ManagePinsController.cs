using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolRecognition.Extensions;
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
        public async Task<IActionResult> ViewPins(Guid id, string sortOrder, string searchQuery, int? pageNumber)
        {

            List<int> _pageIndex = new List<int>();
            //Setting default pag
            int _pageNumber = 0;
            int _currentPage = 1;

            //Setting default values for CustomPagedList
            long _lowerLimit = 0;
            long _upperLimit = 0;
            long _count = 0;
            List<int> _paginationIndices = new List<int>();
            List<PinsViewDto> pins = new List<PinsViewDto>();
            CustomPagedList<PinsViewDto> result = new CustomPagedList<PinsViewDto>();
            //
            RecognitionTypesViewPinsDto recognitionType = null;


            if (id != null && id != Guid.Empty)
            {
                //Setting page number
                if (pageNumber != null && pageNumber.Value >= 0)
                {
                    _pageNumber = pageNumber.Value;
                    _currentPage = pageNumber.Value;
                }

                //Handling search queries
                string _searchQuery = "";
                if (searchQuery != null)
                {
                    _searchQuery = searchQuery;
                }

                if (_pageNumber > 0)
                {
                    _pageNumber = _pageNumber - 1;
                }


                //var recognitionType = await _recognitionTypesService.Get(id);
                switch (sortOrder)
                {
                    case "date_desc":
                        recognitionType = await _recognitionTypesService.GetDetailsAndIncludePinsAndOrderByDateCreated(id, _pageNumber, _searchQuery, true);
                        break;
                    case "date":
                        recognitionType = await _recognitionTypesService.GetDetailsAndIncludePinsAndOrderByDateCreated(id, _pageNumber, _searchQuery, false);
                        break;
                    case "serial_number_desc":
                        recognitionType = await _recognitionTypesService.GetDetailsAndIncludePinsAndOrderBySerialPin(id, _pageNumber, _searchQuery, true);
                        break;
                    case "serial_number":
                        recognitionType = await _recognitionTypesService.GetDetailsAndIncludePinsAndOrderBySerialPin(id, _pageNumber, _searchQuery, true);
                        break;
                    default:
                        recognitionType = await _recognitionTypesService.GetDetailsAndIncludePins(id, _pageNumber, _searchQuery);
                        break;
                }
                if (recognitionType != null)
                {
                    ViewData["RecognitionType"] = recognitionType;
                    result = recognitionType.RecognitionTypePins;
                    //Paging Pins
                    pins = result.Entitys;
                    //
                    _lowerLimit = result.LowerLimit;
                    _upperLimit = result.UpperLimit;
                    _count = result.TotalDBEntitysCount;
                    //
                    _paginationIndices = result.PaginationIndices;

                    //Enabling Manual Paging
                    if (_paginationIndices.Count() > 5)
                    {
                        if ((_currentPage + 2) >= _paginationIndices.Count())
                        {
                            _pageIndex = _paginationIndices.Skip(_paginationIndices.Count() - 5).Take(5).ToList();
                        }
                        else if ((_currentPage - 2) <= 1)
                        {
                            _pageIndex = _paginationIndices.Take(5).ToList();
                        }
                        else
                        {
                            _pageIndex = _paginationIndices.Skip(_currentPage - 2).Take(5).ToList();
                        }
                    }
                    else
                    {
                        _pageIndex = _paginationIndices;
                    }


                    ViewBag.CurrentPage = _currentPage;
                    //
                    ViewBag.LowerLimit = _lowerLimit;
                    ViewBag.UpperLimit = _upperLimit;
                    //
                    ViewBag.SortOrder = sortOrder;
                    ViewBag.SearchQuery = searchQuery;
                    ViewBag.Count = _count;
                    ViewBag.FinalPage = _paginationIndices.Count();
                    ViewBag.PaginationIndices = _pageIndex;
                    //
                    return View(pins);
                }
                else
                {
                    _flashMessage.Info("The requested resource could not be found!");
                    return RedirectToAction("Index", "ManagePins");
                }
            }
            _flashMessage.Info("The requested resource could not be found!");
            return RedirectToAction("Index", "ManagePins");
        }

        //Create
        public async Task<IActionResult> GeneratePins(Guid id)
        {
            var recognitionTypes = await _recognitionTypesService.Get();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GeneratePins(PinsCreateDto model)
        {

            var recognitionTypes = await _recognitionTypesService.Get();
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