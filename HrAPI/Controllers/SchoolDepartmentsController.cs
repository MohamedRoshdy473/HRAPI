using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HrAPI.Models;
using HrAPI.DTO;

namespace HrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolDepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SchoolDepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SchoolDepartments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolDepartmentsDTO>>> GetSchoolDepartments()
        {
            var schoolDepartments = await _context.SchoolDepartments.Include(f => f.School)
               .Select(sc => new SchoolDepartmentsDTO
               {
                   Id = sc.Id,
                   SchoolId = sc.SchoolId,
                   SchoolDepartmentName = sc.SchoolDepartmentName,
                   SchoolName = sc.School.SchoolName
               }).ToListAsync();
            return schoolDepartments;
        }

        // GET: api/SchoolDepartments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolDepartmentsDTO>> GetSchoolDepartments(int id)
        {
            var sc = await _context.SchoolDepartments.Include(f => f.School).FirstOrDefaultAsync(f => f.Id == id);

            var schoolDepartmentsDTO=new SchoolDepartmentsDTO
                           {
                               Id = sc.Id,
                               SchoolId = sc.SchoolId,
                               SchoolDepartmentName = sc.SchoolDepartmentName,
                               SchoolName = sc.School.SchoolName
                           };
            if (sc == null)
            {
                return NotFound();
            }

            return schoolDepartmentsDTO;
        }
        [Route("GetAllSchoolDepartmentsBySchoolId/{SchoolId}")]
        public async Task<IEnumerable<SchoolDepartmentsDTO>> GetAllSchoolDepartmentsBySchoolId(int SchoolId)
        {
            var schoolDepartments = await _context.SchoolDepartments.Include(f => f.School).Where(f => f.SchoolId == SchoolId)
                .Select(sc => new SchoolDepartmentsDTO
                {
                    Id = sc.Id,
                    SchoolId = sc.SchoolId,
                    SchoolDepartmentName = sc.SchoolDepartmentName,
                    SchoolName = sc.School.SchoolName
                }).ToListAsync();

            return schoolDepartments;
        }

        // PUT: api/SchoolDepartments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchoolDepartments(int id, SchoolDepartments schoolDepartments)
        {
            if (id != schoolDepartments.Id)
            {
                return BadRequest();
            }

            _context.Entry(schoolDepartments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolDepartmentsExists(id))
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

        // POST: api/SchoolDepartments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SchoolDepartments>> PostSchoolDepartments(SchoolDepartments schoolDepartments)
        {
            _context.SchoolDepartments.Add(schoolDepartments);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchoolDepartments", new { id = schoolDepartments.Id }, schoolDepartments);
        }

        // DELETE: api/SchoolDepartments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SchoolDepartments>> DeleteSchoolDepartments(int id)
        {
            var schoolDepartments = await _context.SchoolDepartments.FindAsync(id);
            if (schoolDepartments == null)
            {
                return NotFound();
            }

            _context.SchoolDepartments.Remove(schoolDepartments);
            await _context.SaveChangesAsync();

            return schoolDepartments;
        }

        private bool SchoolDepartmentsExists(int id)
        {
            return _context.SchoolDepartments.Any(e => e.Id == id);
        }
    }
}
