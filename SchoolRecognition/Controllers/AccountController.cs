using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolRecognition.Classes;
using SchoolRecognition.Models;
using SchoolRecognition.Repository;

namespace SchoolRecognition.Controllers
{
    public class AccountController : Controller
    {
        IAccount _accountRepo;

        public AccountController(IAccount accountRepo)
        {
            _accountRepo = accountRepo;
        }
        public IActionResult Index()
        {
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
                _accountRepo.CreateUser(model);
            }
            return View();
        }
    }
}