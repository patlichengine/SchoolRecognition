using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.DbContexts;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;

namespace SchoolRecognition.Controllers
{
    [Route("manage_offices")]
    public class OfficesController : Controller
    {
        private readonly SchoolRecognitionContext _context;

        public OfficesController(SchoolRecognitionContext context)
        {
            _context = context;
        }

        // GET: Offices
        [Route("")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Route("details/{id?}")]
        // GET: Offices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var office = new OfficesViewDto()
            {
                Id = new Guid("9c338b95-d8b1-479f-8104-54e23de90283"),
                OfficeName = "Yaba Office",
                OfficeAddress = "21 Hussey Rd, Yaba 100001, Lagos",

            };

            return View(office);
        }

        // GET: Offices/Create
        [Route("create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,StateId,DateCreated,CreatedBy")] Offices offices)
        {
            if (ModelState.IsValid)
            {
                offices.Id = Guid.NewGuid();
                _context.Add(offices);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(offices);
        }

        // GET: Offices/Edit/5

        [Route("update/{id?}")]
        [HttpGet]
        public async Task<IActionResult> Update(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var office = new OfficesCreateDto()
            {
                Id = new Guid("9c338b95-d8b1-479f-8104-54e23de90283"),
                OfficeName = "Yaba Office",
                OfficeAddress = "21 Hussey Rd, Yaba 100001, Lagos",

            };
            return View(office);
        }

        // POST: Offices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Route("update/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Address,StateId,DateCreated,CreatedBy")] Offices offices)
        {
            if (id != offices.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offices);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficesExists(offices.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(offices);
        }

        // GET: Offices/Delete/5
        [Route("delete/{id?}")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var office = new OfficesViewDto()
            {
                Id = new Guid("9c338b95-d8b1-479f-8104-54e23de90283"),
                OfficeName = "Yaba Office",
                OfficeAddress = "21 Hussey Rd, Yaba 100001, Lagos",

            };

            return View(office);
        }

        // POST: Offices/Delete/5
        [Route("delete/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var offices = await _context.Offices.FindAsync(id);
            _context.Offices.Remove(offices);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfficesExists(Guid id)
        {
            return _context.Offices.Any(e => e.Id == id);
        }
    }
}
