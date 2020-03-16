using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolRecognition.Classes;
using SchoolRecognition.Models;

namespace SchoolRecognition.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MakeSchoolPayment()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult MakeSchoolPayment(SchoolPaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                new clsAccount().CreatePayment(model);
            }
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                new clsAccount().CreateUser(model);
            }
            return View();
        }
    }
}