using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolRecognition.Classes;
using SchoolRecognition.Models;
using SchoolRecognition.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolRecognition.Controllers
{
    public class PaymentController : Controller
    {
        IPayment _paymentRepo;
        public PaymentController(IPayment paymentRepo)
        {
            _paymentRepo = paymentRepo;
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
    //};
        public async Task<IActionResult> AddSchoolPayment()
        {
            var recTypes = await _paymentRepo.GetAllRecognitionType();
            ViewBag.RecognitionTypeID = new SelectList(recTypes, "ID", "Name");

            return View();
        }
        [HttpPost]
        public IActionResult AddSchoolPayment(SchoolPaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                _paymentRepo.AddSchoolPayment(model);
            }
            return View();
        }
    }
}
