using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolRecognition.Classes;
using SchoolRecognition.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolRecognition.Controllers
{
    public class PaymentController : Controller
    {
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
        public IActionResult AddSchoolPayment()
        {
           // ViewBag.RecognitionTypeID = new SelectList(clsContent.GetAllRecognitionType(), "ID", "Name");

            return View();
        }
        [HttpPost]
        public IActionResult AddSchoolPayment(SchoolPaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                new clsContent().AddSchoolPayment(model);
            }
            return View();
        }
    }
}
