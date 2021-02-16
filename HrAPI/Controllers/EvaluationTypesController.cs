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
    public class EvaluationTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EvaluationTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EvaluationTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluationType>>> GetEvaluationTypes()
        {
            return await  _context.EvaluationTypes.ToListAsync();
        }

        // GET: api/EvaluationTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluationType>> GetEvaluationType(int id)
        {
            var evaluationType = await _context.EvaluationTypes.FindAsync(id);

            if (evaluationType == null)
            {
                return NotFound();
            }

            return evaluationType;
        }

        // PUT: api/EvaluationTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvaluationType(int id, EvaluationType evaluationType)
        {
            if (id != evaluationType.ID)
            {
                return BadRequest();
            }

            _context.Entry(evaluationType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvaluationTypeExists(id))
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

        // POST: api/EvaluationTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EvaluationType>> PostEvaluationType(EvaluationType evaluationType)
        {
            _context.EvaluationTypes.Add(evaluationType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvaluationType", new { id = evaluationType.ID }, evaluationType);
        }

        // DELETE: api/EvaluationTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EvaluationType>> DeleteEvaluationType(int id)
        {
            var evaluationType = await _context.EvaluationTypes.FindAsync(id);
            if (evaluationType == null)
            {
                return NotFound();
            }

            _context.EvaluationTypes.Remove(evaluationType);
            await _context.SaveChangesAsync();

            return evaluationType;
        }

        private bool EvaluationTypeExists(int id)
        {
            return _context.EvaluationTypes.Any(e => e.ID == id);
        }
    }
}
