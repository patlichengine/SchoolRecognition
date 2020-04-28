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
    public class AccountsController : Controller
    {
        IAccountsRepository _accountRepository;

        public AccountsController(IAccountsRepository accountRepository)
        {
            _accountRepository = accountRepository ??
               throw new ArgumentNullException(nameof(accountRepository));
        }
        public IActionResult Index()
        {
            return RedirectToAction("Register");
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
                _accountRepository.CreateAccount(model);
                //new clsAccount().CreateUser(model);
            }
            return View();
        }
    }
}