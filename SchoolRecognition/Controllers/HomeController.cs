using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using SchoolRecognition.Helpers;
using SchoolRecognition.Services;
using Vereyon.Web;

namespace SchoolRecognition.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFlashMessage _flashMessage;
        private readonly IAccountsRepository _accountRepository;

        public HomeController(ILogger<HomeController> logger, IAccountsRepository accountRepository, IFlashMessage flashMessage)
        {
            _logger = logger;
            _flashMessage = flashMessage ?? throw new ArgumentNullException(nameof(flashMessage));
            _accountRepository = accountRepository ??
               throw new ArgumentNullException(nameof(accountRepository));
        }

        //[Route("login")]
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            //check if the model is valid
            if (ModelState.IsValid)
            {
                var userId = new Guid();
                var result = _accountRepository.GetAccount(model.Email, model.Password).Result;

                if (result != null)
                {
                    //Set the session variable
                    HttpContext.Session.Set<string>("UserId", $"{ result.Id }");

                    //Check if the url home for the selected user exists
                    string[] url = result.Role.UrlHome.Split("/");
                    if (url.Count() > 1)
                    {
                        //_flashMessage.Info("User login successful. Please while the system redirects you to the next page");
                        return RedirectToAction(url[1], url[0]);
                    }
                    else
                    {
                        _flashMessage.Info("User login successful. The user module for this user has not been activated. Please try again later");
                    }

                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    _flashMessage.Danger("Oops...Account Information does not exist!", "Invalid Account");
                    var msg = new MessageViewModel { Title = "Invalid Account", Message = "Oops...Account Information does not exist!" };
                    ViewBag.Message = msg;
                    return View(model);
                }
            }
            _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly...");
            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
