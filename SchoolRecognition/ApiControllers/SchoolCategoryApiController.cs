using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.Data;
using SchoolRecognition.Models;

namespace SchoolRecognition.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolCategoryApiController : ControllerBase
    {
        private readonly SchoolRecognitionAppContext _context;

        public SchoolCategoryApiController(SchoolRecognitionAppContext context)
        {
            _context = context;
        }

        // GET: api/SchoolCategoryApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolCategories>>> GetSchoolCategories()
        {
            return await _context.SchoolCategories.ToListAsync();
        }

        // GET: api/SchoolCategoryApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolCategories>> GetSchoolCategories(Guid id)
        {
            var schoolCategories = await _context.SchoolCategories.FindAsync(id);

            if (schoolCategories == null)
            {
                return NotFound();
            }

            return schoolCategories;
        }

        // PUT: api/SchoolCategoryApi/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchoolCategories(Guid id, SchoolCategories schoolCategories)
        {
            if (id != schoolCategories.Id)
            {
                return BadRequest();
            }

            _context.Entry(schoolCategories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolCategoriesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SchoolCategoryApi
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SchoolCategories>> PostSchoolCategories(SchoolCategories schoolCategories)
        {
            _context.SchoolCategories.Add(schoolCategories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchoolCategories", new { id = schoolCategories.Id }, schoolCategories);
        }

        // DELETE: api/SchoolCategoryApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SchoolCategories>> DeleteSchoolCategories(Guid id)
        {
            var schoolCategories = await _context.SchoolCategories.FindAsync(id);
            if (schoolCategories == null)
            {
                return NotFound();
            }

            _context.SchoolCategories.Remove(schoolCategories);
            await _context.SaveChangesAsync();

            return schoolCategories;
        }

        private bool SchoolCategoriesExists(Guid id)
        {
            return _context.SchoolCategories.Any(e => e.Id == id);
        }
    }
}
