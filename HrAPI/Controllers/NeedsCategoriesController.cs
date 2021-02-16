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
    public class NeedsCategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NeedsCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/NeedsCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NeedsCategory>>> GetNeedsCategory()
        {
            return await _context.NeedsCategory.ToListAsync();
        }

        // GET: api/NeedsCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NeedsCategory>> GetNeedsCategory(int id)
        {
            var needsCategory = await _context.NeedsCategory.FindAsync(id);

            if (needsCategory == null)
            {
                return NotFound();
            }

            return needsCategory;
        }

        // PUT: api/NeedsCategories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNeedsCategory(int id, NeedsCategory needsCategory)
        {
            if (id != needsCategory.ID)
            {
                return BadRequest();
            }

            _context.Entry(needsCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NeedsCategoryExists(id))
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

        // POST: api/NeedsCategories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<NeedsCategory>> PostNeedsCategory(NeedsCategory needsCategory)
        {
            _context.NeedsCategory.Add(needsCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNeedsCategory", new { id = needsCategory.ID }, needsCategory);
        }

        // DELETE: api/NeedsCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NeedsCategory>> DeleteNeedsCategory(int id)
        {
            var needsCategory = await _context.NeedsCategory.FindAsync(id);
            if (needsCategory == null)
            {
                return NotFound();
            }

            _context.NeedsCategory.Remove(needsCategory);
            await _context.SaveChangesAsync();

            return needsCategory;
        }

        private bool NeedsCategoryExists(int id)
        {
            return _context.NeedsCategory.Any(e => e.ID == id);
        }
    }
}
