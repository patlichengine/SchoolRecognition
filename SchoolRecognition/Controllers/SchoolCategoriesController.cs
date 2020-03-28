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
using Vereyon.Web;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolRecognition.Controllers
{
    public class SchoolCategoriesController : Controller
    {
        


        private SchoolCategoriesRepo schoolCategoriess;
        private IFlashMessage _flashMessage;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public SchoolCategoriesController( IHostingEnvironment hostingEnvironment, SchoolCategoriesRepo schoolCategory, IFlashMessage flashMessage)
        {
            
            _hostingEnvironment = hostingEnvironment;
            schoolCategoriess = schoolCategory;

            _flashMessage = flashMessage;


        }

        
        public async Task<IActionResult> Index()
        {
            var result = await schoolCategoriess.ListAll();
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SchoolCategories model)
        {
            if (ModelState.IsValid)
            {
                 await schoolCategoriess.Create(model);
                _flashMessage.Confirmation("New Category Added Successfully! As: ", model.Name);
               
                return RedirectToAction(nameof(Index));
            }
            //else
            //{
            //    await schoolCategoriess.Update(model);
            //}
            return View(model);
        }

        public IActionResult Edit()
        {
            
            return View();
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id)

        {
            SchoolCategories model = new SchoolCategories();
            

            if (id == null)
            {
             
            return View(model);
            }
            
                 model = await schoolCategoriess.GetBySchoolCategoriesId(id);
          if(model == null)
            {
                return NotFound();
            }

            return View(model);
           
      
        }


        // GET: SchoolCategory/Details/5
        public async Task<IActionResult> Details(Guid schoolCategoriesId)
        {
            if(schoolCategoriesId == null)
            {
                return NotFound();
               
            }
            var model = await schoolCategoriess.GetBySchoolCategoriesId(schoolCategoriesId);
            // SchoolCategories model = await schoolCategoriess.GetBySchoolCategoriesId(schoolCategoriesId);

            return View(model);
        }

        public async Task<IActionResult> Delete(Guid schoolCategoriesId)
        {
            var result = await schoolCategoriess.Delete(schoolCategoriesId);
            return View(result);
        }

    }
}
