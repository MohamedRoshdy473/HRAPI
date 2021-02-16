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
    public class LeavesTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LeavesTypesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeavesTypes>>> GetLeaveTypes()
        {
            return await _context.LeavesTypes.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LeavesTypes>> GetLeaveType(int id)
        {
            var leavesTypes = await _context.LeavesTypes.FindAsync(id);

            if (leavesTypes == null)
            {
                return NotFound();
            }

            return leavesTypes;
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeavesType(int id, LeavesTypes leavesTypes)
        {
            if (id != leavesTypes.ID)
            {
                return BadRequest();
            }
            _context.Entry(leavesTypes).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<LeavesTypes>> PostLeavesTypes(LeavesTypes leavesType)
        {
            _context.LeavesTypes.Add(leavesType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeaveTypes", new { id = leavesType.ID }, leavesType);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<LeavesTypes>> DeleteLeavesType(int id)
        {
            var LeavesType = await _context.LeavesTypes.FindAsync(id);
            if (LeavesType == null)
            {
                return NotFound();
            }

            _context.LeavesTypes.Remove(LeavesType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeavesTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return LeavesType;
        }

        private bool LeavesTypeExists(int id)
        {
            return _context.LeavesTypes.Any(e => e.ID == id);
        }
    }
}
