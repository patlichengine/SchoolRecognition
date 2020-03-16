using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Hosting.
using Microsoft.AspNetCore.Mvc;
using SchoolRecognition.Models;
using SchoolRecognition.Repository;
using SchoolRecognition.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolRecognition.Controllers
{
    public class SchoolCategoriesController : Controller
    {
        // GET: /<controller>/


        private readonly Services.SchoolCategoriesService schoolCategoriess;
        private readonly SchoolCategoriesRepo _schoolCategories;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public SchoolCategoriesController(SchoolCategoriesRepo schoolCategories, IHostingEnvironment hostingEnvironment, Services.SchoolCategoriesService schoolCategory)
        {
            _schoolCategories = schoolCategories;
            _hostingEnvironment = hostingEnvironment;
            schoolCategoriess = schoolCategory;


        }

        
        public  IEnumerable<SchoolCategories> Index()
        {
            return _schoolCategories.ListAll();
        }
        public IActionResult Categories()
        {
            return View();
        }

        public IActionResult Categories(Models.SchoolCategories schoolCategorie)
        {
            _schoolCategories.Create(schoolCategorie);
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
    }
}
