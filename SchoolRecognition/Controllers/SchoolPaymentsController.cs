using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolRecognition.Classes;
using SchoolRecognition.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SchoolRecognition.Services;
using SchoolRecognition.Entities;
using Vereyon.Web;
using AutoMapper;
using SchoolRecognition.Helpers;
using SchoolRecognition.ResourceParameters;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Http;
using Humanizer;
using NToastNotify;
using ImageMagick;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolRecognition.Controllers
{

    [Route("manage_payments")]
    public class SchoolPaymentsController : Controller
    {
        private IFlashMessage _flashMessage;
        private readonly IToastNotification _toastNotification;
        private IPinsRepository _pinsRepository;
        private IRecognitionTypesRepository _recognitionTypesRepository;
        private ISchoolPaymentsRepository _schoolPaymentsRepository;
        private ISchoolsRepository _schoolsRepository;
        private ISchoolCategoryRepository _schoolCategorysRepository;
        private ICentresRepository _centresRepository;
        private readonly IMapper _mapper;
        private byte[] paymentReceiptImage = new byte[] { };
        private int defaultCroppedImageWidth = 500;

        //
        private int _defaultFileSizeLimit = 1100000;

        public SchoolPaymentsController(IFlashMessage flashMessage, IToastNotification toastNotification, IPinsRepository pinsRepository, IRecognitionTypesRepository recognitionTypesRepository, ISchoolPaymentsRepository schoolPaymentsRepository, ISchoolCategoryRepository schoolCategorysRepository, ISchoolsRepository schoolsRepository, ICentresRepository centresRepository, IMapper mapper)
        {
            _flashMessage = flashMessage ??
                throw new ArgumentNullException(nameof(mapper));
            _toastNotification = toastNotification ??
                throw new ArgumentNullException(nameof(toastNotification));
            _pinsRepository = pinsRepository ??
                throw new ArgumentNullException(nameof(pinsRepository));
            _recognitionTypesRepository = recognitionTypesRepository ??
               throw new ArgumentNullException(nameof(recognitionTypesRepository));
            _schoolPaymentsRepository = schoolPaymentsRepository ??
               throw new ArgumentNullException(nameof(schoolPaymentsRepository));
            _schoolsRepository = schoolsRepository ??
               throw new ArgumentNullException(nameof(schoolsRepository));
            _schoolCategorysRepository = schoolCategorysRepository ??
               throw new ArgumentNullException(nameof(schoolCategorysRepository));
            _centresRepository = centresRepository ??
               throw new ArgumentNullException(nameof(centresRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        // GET: SchoolPayments
        [Route("")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Index(string orderBy, string searchQuery, int? pageNumber)
        {
            try
            {



                IEnumerable<int> pages = new List<int>();

                var resourceParams = new SchoolPaymentsResourceParams()
                {
                    PageNumber = pageNumber != null ? pageNumber.Value : 1,
                    SearchQuery = !String.IsNullOrWhiteSpace(searchQuery) ? searchQuery : null,
                    OrderBy = !String.IsNullOrWhiteSpace(orderBy) ? searchQuery : "DateCreatedDesc",
                };
                //Instantiate PagedList
                PagedList<SchoolPaymentsViewDto> schoolPayments = PagedList<SchoolPaymentsViewDto>
                         .Create(Enumerable.Empty<SchoolPaymentsViewDto>().AsQueryable(),
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
                    case "receipt_no_desc":
                        resourceParams.OrderBy = "ReceiptNoDesc";
                        break;
                    case "receipt_no":
                        resourceParams.OrderBy = "ReceiptNo";
                        break;
                    default:
                        resourceParams.OrderBy = "DateCreated";
                        break;
                }

                var result = await _schoolPaymentsRepository.PagedList(resourceParams);

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

                schoolPayments = result;

                ViewData["Pages"] = pages;
                ViewData["OrderBy"] = orderBy;
                ViewData["SearchQuery"] = searchQuery;


                return PartialView(schoolPayments);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [Route("details/{id:guid}")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        // GET: SchoolPayments/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {

                if (id == Guid.Empty)
                {
                    return BadRequest();
                }

                var payment = await _schoolPaymentsRepository.Get(id);

                if (payment == null)
                {
                    return NotFound();
                }


                return PartialView(payment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: SchoolPayments/Create
        [Route("make_payment")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Create()
        {
            try
            {
                #region SelectLists

                var recognitionTypes = await _recognitionTypesRepository.List();

                //
                ViewBag.RecognitionTypes = recognitionTypes.OrderBy(x => x.RecognitionTypeName).Select(x =>
                 new SelectListItem()
                 {
                     Text = $"{x.RecognitionTypeName}",
                     Value = x.Id.ToString(),
                 }).ToList();


                #endregion


                _flashMessage.Info("Hint:", "All uploaded files must be less than {0}", (_defaultFileSizeLimit).Bytes().Humanize("0.00"));
                return PartialView();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("make_payment")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SchoolPaymentsCreateDto model, IFormFile uploadedFile)
        {


            #region SelectLists

            var recognitionTypes = await _recognitionTypesRepository.List();

            //
            ViewBag.RecognitionTypes = recognitionTypes.OrderBy(x => x.RecognitionTypeName).Select(x =>
             new SelectListItem()
             {
                 Text = $"{x.RecognitionTypeName}",
                 Value = x.Id.ToString(),
             }).ToList();


            #endregion
            var url = Url.Action("Create");
            try
            {
                if (uploadedFile == null)
                {

                    _flashMessage.Danger("Oops...Something went wrong!", "Upload a valid RECEIPT IMAGE!");
                    return Json(url);
                }
                if (uploadedFile != null && uploadedFile.Length > _defaultFileSizeLimit)
                {
                    _flashMessage.Danger("File is too large!", "Your file is {0}! File must be less than {1}", (uploadedFile.Length).Bytes().Humanize("0.00"), (_defaultFileSizeLimit).Bytes().Humanize("0.00"));
                    return Json(url);
                }
                if (ModelState.IsValid)
                {


                    if (model.RecognitionTypeId == null)
                    {

                        _flashMessage.Danger("Oops...Something went wrong!", "Attempted operation with incomplete or invalid data!");
                        return Json(url);
                    }
                    //Check if pins are available
                    if ((await _pinsRepository.CheckTotalActivePinsNOTInUse(model.RecognitionTypeId.Value)) < 1)
                    {

                        _flashMessage.Danger("Operation Halted!", "There are no PINS available for this payment!");
                        return Json(url);
                    }
                    //Check if entry with similar data already exists
                    if (await _schoolPaymentsRepository.Exists(model.PaymentReceiptNo))
                    {

                        _flashMessage.Danger("Duplicate Data Entry!", "A RECEIPT with this Reciept Number has already been used in the system...");
                        return Json(url);
                    }
                    if (model.SchoolId == null && String.IsNullOrWhiteSpace(model.SchoolName))
                    {

                        _flashMessage.Danger("Oops...Something went wrong!", "Attempted operation with incomplete or invalid data!");
                        return Json(url);
                    }

                    if (String.IsNullOrWhiteSpace(model.SchoolName) && await _schoolsRepository.Exists(model.SchoolName))
                    {

                        _flashMessage.Danger("Duplicate Data Entry!", "A SCHOOL with this NAME has already exists in the system...");
                        return Json(url);
                    }

                    #region Handling Image File


                    using (var ms = new MemoryStream())
                    {
                                                
                        uploadedFile.CopyTo(ms);



                        byte[] fileBytes = ms.ToArray();

                        paymentReceiptImage = ImageFileHelper.CompressAndResizeImageFromMemoryStream(fileBytes, defaultCroppedImageWidth);


                        // act on the Base64 data
                    }

                    #endregion

                    model.PaymentReceiptImage = paymentReceiptImage;

                    var result = await _schoolPaymentsRepository.Create(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "New Payment Made Successfully!");
                        url = Url.Action("Index", "SchoolPayments");
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




        #region SelectListResolver


        [Route("resolve_recognition_type")]
        [HttpGet]
        public async Task<IActionResult> ResolvePartialView(string recognitionTypeName)
        {


            #region SelectLists

            var creationDependencys = await _schoolPaymentsRepository.GetCreationDependencys();


            var schoolCategorys = creationDependencys.SchoolCategorys;
            var officeLocalGovernments = creationDependencys.OfficeLocalGovernments;

            //
            ViewBag.SchoolCategorys = schoolCategorys.OrderBy(x => x.Name).Select(x =>
             new SelectListItem()
             {
                 Text = x.Name,
                 Value = x.Id.ToString(),
             }).ToList();


            //
            ViewBag.OfficeLocalGovernments = officeLocalGovernments.OrderBy(x => x.LocalGovernmentCode).Select(x =>
             new SelectListItem()
             {
                 Text = $"{x.LocalGovernmentCode} {x.LocalGovernmentName} [ {x.StateCode} {x.StateName} ]",
                 Value = x.LocalGovernmentId.ToString(),
             }).ToList();

            #endregion

            if (!String.IsNullOrWhiteSpace(recognitionTypeName))
            {

                string _recognitionTypeName = recognitionTypeName.ToLower().MakeAlphaNumeric();

                _recognitionTypeName = _recognitionTypeName.Camelize();
                string partialView = $"_{_recognitionTypeName}";

                if (_recognitionTypeName == "derecognition")
                {

                    _toastNotification.AddInfoToastMessage($"Payment for {_recognitionTypeName} is not supported as the moment!");

                }

                return PartialView(partialView);
            }

            return BadRequest();
        }
        


        [Route("resolve_centre")]
        [HttpGet]
        public async Task<IActionResult> ValidateCentreNo(string centreNo)
        {



            if (!String.IsNullOrWhiteSpace(centreNo))
            {

                var centre = await _centresRepository.GetByCentreNumber(centreNo);

                if (centre == null)
                {


                    _toastNotification.AddErrorToastMessage($"Invalid Centre Number!");

                    return NotFound();
                }

                var result = new 
                {
                    SchoolName = centre.CentreName
                };

                return Json(result);
            }

            return BadRequest();
        }



        #endregion

    }
}
