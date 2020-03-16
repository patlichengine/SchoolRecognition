using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.Data;
using SchoolRecognition.Models;

namespace SchoolRecognition.Controllers
{
    public class SchoolCategoryController : Controller
    {
        private readonly SchoolRecognitionAppContext _context;

        public SchoolCategoryController(SchoolRecognitionAppContext context)
        {
            _context = context;
        }

        // GET: SchoolCategory
        public async Task<IActionResult> Index()
        {
            return View(await _context.SchoolCategories.ToListAsync());
        }

        // GET: SchoolCategory/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolCategories = await _context.SchoolCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolCategories == null)
            {
                return NotFound();
            }

            return View(schoolCategories);
        }

        // GET: SchoolCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SchoolCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Code")] SchoolCategories schoolCategories)
        {
            if (ModelState.IsValid)
            {
                schoolCategories.Id = Guid.NewGuid();
                _context.Add(schoolCategories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schoolCategories);
        }

        // GET: SchoolCategory/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolCategories = await _context.SchoolCategories.FindAsync(id);
            if (schoolCategories == null)
            {
                return NotFound();
            }
            return View(schoolCategories);
        }

        // POST: SchoolCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Code")] SchoolCategories schoolCategories)
        {
            if (id != schoolCategories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schoolCategories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolCategoriesExists(schoolCategories.Id))
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
            return View(schoolCategories);
        }

        // GET: SchoolCategory/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolCategories = await _context.SchoolCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolCategories == null)
            {
                return NotFound();
            }

            return View(schoolCategories);
        }

        // POST: SchoolCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var schoolCategories = await _context.SchoolCategories.FindAsync(id);
            _context.SchoolCategories.Remove(schoolCategories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolCategoriesExists(Guid id)
        {
            return _context.SchoolCategories.Any(e => e.Id == id);
        }
    }
}
