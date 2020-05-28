using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRecognition.Models;
using SchoolRecognition.Services;

namespace SchoolRecognition.Controllers
{
    public class SchoolController : Controller
    {
        private readonly ISchoolsRepository _schoolsRepository;

        public SchoolController(ISchoolsRepository schoolsRepository)
        {
            _schoolsRepository = schoolsRepository;
        }

        // GET: School
        public async Task<IActionResult> Index()
        {
            var result = await _schoolsRepository.List();
            return PartialView(result);
        }

        // GET: School/Details/5
        public ActionResult Details(int id)
        {
            return PartialView();
        }

        // GET: School/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: School/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<SchoolsViewDto>> Create(SchoolsCreateDto createSchoolsDto)
        {
            try
            {
                // TODO: Add insert logic here
                await _schoolsRepository.Create(createSchoolsDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return PartialView();
            }
        }

        // GET: School/Edit/5
        public async Task<ActionResult<SchoolsViewDto>> Edit(Guid id)
        {
            var model = await _schoolsRepository.Get(id);

            if (model == null)
            {
                return NotFound();
            }

            return PartialView(model);
        }

        // POST: School/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SchoolsCreateDto school)
        {
            try
            {
                // TODO: Add update logic here
                await _schoolsRepository.Update(school);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return PartialView();
            }
        }

        // GET: School/Delete/5
        public ActionResult Delete(Guid id)
        {
            _schoolsRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: School/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return PartialView();
            }
        }
    }
}