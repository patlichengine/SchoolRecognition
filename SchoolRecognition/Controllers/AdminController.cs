﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SchoolRecognition.Controllers
{
    public class AdminController : Controller
    {
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }


    }
}