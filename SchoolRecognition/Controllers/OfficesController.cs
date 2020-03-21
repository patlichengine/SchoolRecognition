using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SchoolRecognition.Models;
using SchoolRecognition.Repository;
using Vereyon.Web;

namespace SchoolRecognition.Controllers
{
    public class OfficesController : Controller
    {
        private IOffices _offices;
        private IFlashMessage _flashMessage;

        public OfficesController(IFlashMessage flashMessage, IOffices offices)
        {
            _offices = offices;
            _flashMessage = flashMessage;
        }
        
        public async Task<IActionResult> Index()
        {
            var result = await _offices.ListAll();
            return View(result);
        } 
        
        // GET: Offices/Create
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]

        public async Task<IActionResult> Create(Offices model)
        {
            if(ModelState.IsValid)
            {
                await _offices.Create(model);
                _flashMessage.Confirmation("New Office Added Successfully! As: ", model.Name);

                return RedirectToAction("Index", "Offices");
            }
            return View(model);
        }
        
        // GET: Offices/Edit/5

            public IActionResult Edit()
        {
            return View();
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, Offices model)
        {
        

           
            return View(id);
        }

        // POST: Offices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.


        public IActionResult Offices()
        {
            return View();
        }

        

        public async Task<IActionResult> Offices(Offices model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _offices.Create(model);
            return RedirectToAction("_offices", "Offices");
        }

        
        // GET: Offices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
          


             
            return View(id);
        }

       

        // POST: Offices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        // GET: Offices/Delete/5
     

        // POST: Offices/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            
            return RedirectToAction(nameof(Index));
        }

       
    }
}
