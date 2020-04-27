using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolRecognition.Classes;
using SchoolRecognition.Models;
using SchoolRecognition.Services;

namespace SchoolRecognition.Controllers
{
    public class AccountController : Controller
    {
        private IAccountsRepository _accountsService;

        public AccountController(IAccountsRepository accountsService)
        {
            _accountsService = accountsService;
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
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                _accountsService.CreateAccount(model);
                //new clsAccount().CreateUser(model);
            }
            return View();
        }
    }
}