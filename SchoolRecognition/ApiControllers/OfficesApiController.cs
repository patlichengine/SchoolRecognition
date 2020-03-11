using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.Models;

namespace SchoolRecognition.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficesApiController : ControllerBase
    {
        private readonly SchoolRecognitionContext _context;

        public OfficesApiController(SchoolRecognitionContext context)
        {
            _context = context;
        }

        // GET: api/OfficesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Offices>>> GetOffices()
        {
            return await _context.Offices.ToListAsync();
        }

        // GET: api/OfficesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Offices>> GetOffices(Guid id)
        {
            var offices = await _context.Offices.FindAsync(id);

            if (offices == null)
            {
                return NotFound();
            }

            return offices;
        }

        // PUT: api/OfficesApi/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOffices(Guid id, Offices offices)
        {
            if (id != offices.Id)
            {
                return BadRequest();
            }

            _context.Entry(offices).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficesExists(id))
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

        // POST: api/OfficesApi
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Offices>> PostOffices(Offices offices)
        {
            _context.Offices.Add(offices);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOffices", new { id = offices.Id }, offices);
        }

        // DELETE: api/OfficesApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Offices>> DeleteOffices(Guid id)
        {
            var offices = await _context.Offices.FindAsync(id);
            if (offices == null)
            {
                return NotFound();
            }

            _context.Offices.Remove(offices);
            await _context.SaveChangesAsync();

            return offices;
        }

        private bool OfficesExists(Guid id)
        {
            return _context.Offices.Any(e => e.Id == id);
        }
    }
}
