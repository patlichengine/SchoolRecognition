using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.Models;

namespace SchoolRecognition.Controllers
{
    public class StatesController : Controller
    {
        private readonly SchoolRecognitionContext _context;

        public StatesController(SchoolRecognitionContext context)
        {
            _context = context;
        }

        // GET: States
        public async Task<IActionResult> Index()
        {
            return View(await _context.States.ToListAsync());
        }

        // GET: States/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var states = await _context.States
                .FirstOrDefaultAsync(m => m.Id == id);
            if (states == null)
            {
                return NotFound();
            }

            return View(states);
        }

        // GET: States/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: States/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Code")] States states)
        {
            if (ModelState.IsValid)
            {
                states.Id = Guid.NewGuid();
                _context.Add(states);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(states);
        }

        // GET: States/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var states = await _context.States.FindAsync(id);
            if (states == null)
            {
                return NotFound();
            }
            return View(states);
        }

        // POST: States/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Code")] States states)
        {
            if (id != states.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(states);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatesExists(states.Id))
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
            return View(states);
        }

        // GET: States/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var states = await _context.States
                .FirstOrDefaultAsync(m => m.Id == id);
            if (states == null)
            {
                return NotFound();
            }

            return View(states);
        }

        // POST: States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var states = await _context.States.FindAsync(id);
            _context.States.Remove(states);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatesExists(Guid id)
        {
            return _context.States.Any(e => e.Id == id);
        }
    }
}
