using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SchoolRecognition.Controllers
{

    [Route("manage_locations")]
    public class ManageLocationsController : Controller
    {

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}