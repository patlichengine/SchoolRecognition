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


        private SchoolCategoriesRepo schoolCategoriess;

        

        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public SchoolCategoriesController( IHostingEnvironment hostingEnvironment, SchoolCategoriesRepo schoolCategory)
        {
            
            _hostingEnvironment = hostingEnvironment;
            schoolCategoriess = schoolCategory;


        }

        
        public async Task<IActionResult> Index()
        {
            var result = await schoolCategoriess.ListAll();
            return View(result);
        }
        public IActionResult Categories()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Categories(SchoolCategories model)
        {
            if (ModelState.IsValid)
            {
                 await schoolCategoriess.Create(model);
                return RedirectToAction("Categories", "SchoolCategories");
            }
            return View(model);
        }
        public IActionResult Edit()
        {
            
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(SchoolCategories school)
        {
            var result = await schoolCategoriess.Update(school);
            return RedirectToAction("Edit", "SchoolCategories");
        }


        // GET: SchoolCategory/Details/5
        public async Task<IActionResult> Details(Guid schoolCategoriesId)
        {
            var result = await schoolCategoriess.GetBySchoolCategoriesId(schoolCategoriesId);

            return View(result);
        }

        public async Task<IActionResult> Delete(Guid schoolCategoriesId)
        {
            var result = await schoolCategoriess.Delete(schoolCategoriesId);
            return View(result);
        }

    }
}
