using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HrAPI.Models;

namespace HrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationStatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EducationStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EducationStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EducationStatus>>> GetEducationStatus()
        {
            return await _context.EducationStatus.ToListAsync();
        }

        // GET: api/EducationStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EducationStatus>> GetEducationStatus(int id)
        {
            var educationStatus = await _context.EducationStatus.FindAsync(id);

            if (educationStatus == null)
            {
                return NotFound();
            }

            return educationStatus;
        }

        // PUT: api/EducationStatus/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducationStatus(int id, EducationStatus educationStatus)
        {
            if (id != educationStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(educationStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducationStatusExists(id))
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

        // POST: api/EducationStatus
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EducationStatus>> PostEducationStatus(EducationStatus educationStatus)
        {
            _context.EducationStatus.Add(educationStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEducationStatus", new { id = educationStatus.Id }, educationStatus);
        }

        // DELETE: api/EducationStatus/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EducationStatus>> DeleteEducationStatus(int id)
        {
            var educationStatus = await _context.EducationStatus.FindAsync(id);
            if (educationStatus == null)
            {
                return NotFound();
            }

            _context.EducationStatus.Remove(educationStatus);
            await _context.SaveChangesAsync();

            return educationStatus;
        }

        private bool EducationStatusExists(int id)
        {
            return _context.EducationStatus.Any(e => e.Id == id);
        }
    }
}
