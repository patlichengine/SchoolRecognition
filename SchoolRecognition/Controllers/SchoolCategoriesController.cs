using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Hosting.
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NToastNotify;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;

using SchoolRecognition.Services;
using Vereyon.Web;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolRecognition.Controllers
{
    public class SchoolCategoriesController : Controller
    {


        private readonly IToastNotification _toastNotification;
        private readonly ISchoolCategoryRepository schoolCategories;
        private IFlashMessage _flashMessage;

        private readonly ILogger _logger;


        public SchoolCategoriesController(ISchoolCategoryRepository schoolCategory, IFlashMessage flashMessage, ILogger<SchoolCategoriesController> logger, IToastNotification toastNotification)
        {

            _logger = logger;

            schoolCategories = schoolCategory;

            _flashMessage = flashMessage;

            _toastNotification = toastNotification;


        }


        public async Task<IActionResult> Index()
        {
            var result = await schoolCategories.List();

            return PartialView(result);
        }
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<SchoolCategorysViewDto>> Create(SchoolCategorysCreateDto model)
        {
            if (ModelState.IsValid)
            {
                await schoolCategories.Create(model);
                _toastNotification.AddSuccessToastMessage("New Category Added Successfully!");
                // _flashMessage.Confirmation("New Category Added Successfully! As: ", model.Name);

                return RedirectToAction(nameof(Index));
            }

            return PartialView();
        }

        //[HttpGet("{id}")]
        public async Task<ActionResult<SchoolCategorysViewDto>> Edit(Guid id)
        {


            _logger.LogInformation("Getting item {Id} at {RequestTime}", id, DateTime.Now);
            var model = await schoolCategories.GetCategoryById(id);
            // var model = await _context.SchoolCategories.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return PartialView(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateSchoolCategoryDto school)

        {
            try
            {
                await schoolCategories.Update(id, school);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            return PartialView();


        }


        // GET: SchoolCategory/Details/5
        public async Task<IActionResult> Details(Guid schoolCategoriesId)
        {
            if (schoolCategoriesId == null)
            {
                return NotFound();

            }
            var model = await schoolCategories.GetCategoryById(schoolCategoriesId);


            return PartialView(model);
        }

        #region

        // GET: api/SchoolCategories
        [HttpGet]
        public JsonResult Get()
        {


            var result = schoolCategories.List().Result;

            return new JsonResult(new { data = result });

        }




        //[HttpDelete]
        public JsonResult Delete(Guid id)
        {
            if (!schoolCategories.SchoolCategoriesExists(id).Result)
            {
               // return NotFound();
                return Json(new { success = false, message = "Error while Deleting" });
            }

            schoolCategories.DeleteSchoolCategory(id);
            schoolCategories.Save();

            return Json(new { success = true, message = "Success, Record Deleted " });


            #endregion
        }
    }
}