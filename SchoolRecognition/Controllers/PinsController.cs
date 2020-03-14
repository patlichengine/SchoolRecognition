using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SchoolRecognition.Controllers
{
    public class PinsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(Guid? id)
        {
            return View();
        }

        public async Task<IActionResult> GeneratePins()
        {
            return View();
        }
    }
}