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
    public class PositionLevelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PositionLevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PositionLevels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionLevel>>> GetPositionLevels()
        {
            return await _context.PositionLevels.ToListAsync();
        }

        // GET: api/PositionLevels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PositionLevel>> GetPositionLevel(int id)
        {
            var positionLevel = await _context.PositionLevels.FindAsync(id);

            if (positionLevel == null)
            {
                return NotFound();
            }

            return positionLevel;
        }

        // PUT: api/PositionLevels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPositionLevel(int id, PositionLevel positionLevel)
        {
            if (id != positionLevel.Id)
            {
                return BadRequest();
            }

            _context.Entry(positionLevel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionLevelExists(id))
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

        // POST: api/PositionLevels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PositionLevel>> PostPositionLevel(PositionLevel positionLevel)
        {
            _context.PositionLevels.Add(positionLevel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPositionLevel", new { id = positionLevel.Id }, positionLevel);
        }

        // DELETE: api/PositionLevels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PositionLevel>> DeletePositionLevel(int id)
        {
            var positionLevel = await _context.PositionLevels.FindAsync(id);
            if (positionLevel == null)
            {
                return NotFound();
            }

            _context.PositionLevels.Remove(positionLevel);
            await _context.SaveChangesAsync();

            return positionLevel;
        }

        private bool PositionLevelExists(int id)
        {
            return _context.PositionLevels.Any(e => e.Id == id);
        }
    }
}
