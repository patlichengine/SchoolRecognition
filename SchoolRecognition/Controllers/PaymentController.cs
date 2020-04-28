using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolRecognition.Classes;
using SchoolRecognition.Models;
//using SchoolRecognition.Repository;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SchoolRecognition.Services;
using SchoolRecognition.Entities;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolRecognition.Controllers
{
    public class PaymentController : Controller
    {
       
        private IWebHostEnvironment _env;
        ICentresRepository _centreRepository;
        IRecognitionTypesRepository _recognitionTypeRepository;
        IPinsRepository _pinRepository;
        public PaymentController(ICentresRepository centreRepo, IRecognitionTypesRepository recognitionTypesRepo, IPinsRepository pinsRepo)
        {
            _centreRepository = centreRepo ??
               throw new ArgumentNullException(nameof(centreRepo));

            _recognitionTypeRepository = recognitionTypesRepo ??
              throw new ArgumentNullException(nameof(recognitionTypesRepo));

            _pinRepository = pinsRepo ??
             throw new ArgumentNullException(nameof(recognitionTypesRepo));
        }
       
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //    public List<SelectListItem> GetAllRecognitionType { get; } = new List<SelectListItem>
        //{
        //    new SelectListItem { Value = "1", Text = "GENERAL RECOGNITION" },
        //    new SelectListItem { Value = "2", Text = "SUBJECT RECOGNITION" },
        //    new SelectListItem { Value = "3", Text = "DE-RECOGNITION"  },
        //};GetPINS

        //public async Task<IActionResult> GetPINS(Guid recognitiontypeid)
        //{
        //    var pins = await _pinRepository.GetPins(recognitiontypeid);
        //    pins = (from p in pins select p).ToList();
        //   var pinList = new SelectList(pins, "ID", "SerialPin").ToList();

        //    return Json(pinList);
        //}

        //public async Task<IActionResult> GetCentreNameByCentreNo(string centreno)
        //{
        //    List<RecognitionTypes> recognitionList = new List<RecognitionTypes>();

        //    var centreinfo = await _paymentRepo.GetCentreNameByCentreNo(centreno);
        //     return Json(new {centreinfo});
        //  //  return Json(centreinfo);

        //}

        public async Task<IActionResult> AddSchoolPayment()
        {
           List<RecognitionTypesDto> recognitionList = new List<RecognitionTypesDto>();

            var recTypes = await _recognitionTypeRepository.GetAllRecognitionTypes();
            recognitionList = (from c in recTypes select c).ToList();
            //re.Insert(0, new RecognitionTypes { Id = new Guid("00000000-0000-0000-0000-000000000000"), Name = "Select rec type" });
            // re.Insert(1, new RecognitionTypes {Id=recTypes.First().Id,Name=recTypes.First().Name });
            ViewBag.RecognitionList = recognitionList;



            //model.RecognitionTypes = new SelectList(recTypes, "ID", "Name");
            return View();
        }


        //[HttpPost]
        //public IActionResult AddSchoolPayment(SchoolPaymentViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _paymentRepo.AddSchoolPayment(model);
        //    }
        //    return View();
        //}

        //public IActionResult ReadCentres()
        //{
        //  //  var centres = System.IO.File.ReadAllLines(Server.MapPath("~/WAEC/CentreFile.txt"));
        //    var formattedCentres = new List<CentresModel>();

        //  //  var webRoot = _env.WebRootPath;

        //    var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CentreFile.txt");

        //    string[] centres = System.IO.File.ReadAllLines(file);

        //    //var centres = System.IO.File.ReadAllLines("~/WAEC/CentreFile.txt");



        //    foreach (var centre in centres)
        //    {
        //        var real = centre.Substring(2);
        //        var centreNumber = real.Substring(0, 7);
        //        var cat = real.Substring(7, 1);
        //        var catgory = _paymentRepo.GetSchoolCategoryIDByCode(cat);

        //        var centreName = real.Substring(8);
        //        formattedCentres.Add(new CentresModel { CentreName = centreName, CentreNumber = centreNumber,CategoryID= catgory.Result.ID });
        //      var x=  _paymentRepo.InsertSchoolCentre(centreName, centreNumber, catgory.Result.ID);
        //        //if existing dont save else save

        //    }

        //    //ViewBag.Centres = centres;
        //    return View(formattedCentres);
        //}
    }
}
