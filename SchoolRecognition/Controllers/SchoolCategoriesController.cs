using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Hosting.
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolRecognition.Models;
using SchoolRecognition.Repository;
using SchoolRecognition.Services;
using Vereyon.Web;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolRecognition.Controllers
{
    public class SchoolCategoriesController : Controller
    {
        


        private SchoolCategoriesRepo schoolCategories;
        private IFlashMessage _flashMessage;
        
        private readonly ILogger _logger;
       

        [Obsolete]
        public SchoolCategoriesController(  SchoolCategoriesRepo schoolCategory, IFlashMessage flashMessage, ILogger<SchoolCategoriesController> logger)
        {
            
            _logger = logger;
           
            schoolCategories = schoolCategory;

            _flashMessage = flashMessage;


        }

        
        public async Task<IActionResult> Index()
        {
            var result = await schoolCategories.List();
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
                 await schoolCategories.Create(model);
                _flashMessage.Confirmation("New Category Added Successfully! As: ", model.Name);
               
                return RedirectToAction(nameof(Index));
            }
            //else
            //{
            //    await schoolCategoriess.Update(model);
            //}
            return View(model);
        }

        //[HttpGet("{id}")]
        public async Task<ActionResult<SchoolCategories>> Edit(Guid id)
        {
           

            _logger.LogInformation("Getting item {Id} at {RequestTime}", id, DateTime.Now);
            var model = await schoolCategories.GetById(id);
           // var model = await _context.SchoolCategories.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SchoolCategories school)

        {
            try
            {
               await schoolCategories.Update(school);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            //if (model.Id == null)
            //{

            //return NotFound();
            //}


            var model = await schoolCategories.GetById(id);
          //if(model == null)
          //  {
          //      return NotFound();
          //  }

            return View(model);
           
      
        }


        // GET: SchoolCategory/Details/5
        public async Task<IActionResult> Details(Guid schoolCategoriesId)
        {
            if(schoolCategoriesId == null)
            {
                return NotFound();
               
            }
            var model = await schoolCategories.GetById(schoolCategoriesId);
         

            return View(model);
        }

        public async Task<IActionResult> Delete(SchoolCategories school)
        {
            
            await schoolCategories.Delete(school.Id);
            return RedirectToAction("Index");
           
             
        }

    }
}
