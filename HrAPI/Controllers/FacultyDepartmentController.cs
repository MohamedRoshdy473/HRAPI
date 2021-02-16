using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HrAPI.DTO;
using HrAPI.Models;

namespace HrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyDepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FacultyDepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FacultyDepartment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacultyDepartmentDTO>>> GetFacultyDepartmentDTO()
        {
            return await _context.FacultyDepartments.Include(f => f.Faculty).Include(f => f.Faculty.University).Select(facultyDep => new FacultyDepartmentDTO
            {
                Id = facultyDep.Id,
                FacultyDepartmentName = facultyDep.FacultyDepartmentName,
                FacultyId = facultyDep.FacultyId,
                FacultyName = facultyDep.Faculty.FacultyName,
                UniversityId = facultyDep.Faculty.UniversityID,
                UniversityName = facultyDep.Faculty.University.UniversityName
            }).ToListAsync();
        }

        // GET: api/FacultyDepartment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FacultyDepartmentDTO>> GetFacultyDepartmentDTO(int id)
        {
            var facultyDep = await _context.FacultyDepartments.Include(f => f.Faculty).Include(f => f.Faculty.University).FirstOrDefaultAsync(f => f.Id == id);
            var facultyDepartment = new FacultyDepartmentDTO
            {
                Id = facultyDep.Id,
                FacultyDepartmentName = facultyDep.FacultyDepartmentName,
                FacultyId = facultyDep.FacultyId,
                FacultyName = facultyDep.Faculty.FacultyName,
                UniversityId = facultyDep.Faculty.UniversityID,
                UniversityName = facultyDep.Faculty.University.UniversityName
            };
            if (facultyDep == null)
            {
                return NotFound();
            }

            return facultyDepartment;
        }
        [HttpGet]
        [Route("GetFacultyDepartmentsByFacultyId/{FacultyId}")]
        public IEnumerable<FacultyDepartmentDTO> GetFacultyDepartmentsByFacultyId(int FacultyId)
        {
            var facultyDepartments = _context.FacultyDepartments.Include(f => f.Faculty).Where(f => f.FacultyId == FacultyId).Select(fac => new FacultyDepartmentDTO
            {
                Id = fac.Id,
                FacultyDepartmentName=fac.FacultyDepartmentName
            }).ToList();
            return facultyDepartments;
        }
        // PUT: api/FacultyDepartment/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacultyDepartmentDTO(int id, FacultyDepartmentDTO facultyDepartmentDTO)
        {
            if (id != facultyDepartmentDTO.Id)
            {
                return BadRequest();
            }
            FacultyDepartment facultyDepartment = new FacultyDepartment();
            facultyDepartment.Id = facultyDepartmentDTO.Id;
            facultyDepartment.FacultyDepartmentName = facultyDepartmentDTO.FacultyDepartmentName;
            facultyDepartment.FacultyId = facultyDepartmentDTO.FacultyId;
            _context.Entry(facultyDepartment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacultyDepartmentDTOExists(id))
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

        // POST: api/FacultyDepartment
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FacultyDepartmentDTO>> PostFacultyDepartmentDTO(FacultyDepartmentDTO facultyDepartmentDTO)
        {
            FacultyDepartment facultyDepartment = new FacultyDepartment();
            facultyDepartment.Id = facultyDepartmentDTO.Id;
            facultyDepartment.FacultyDepartmentName = facultyDepartmentDTO.FacultyDepartmentName;
            facultyDepartment.FacultyId = facultyDepartmentDTO.FacultyId;
            _context.FacultyDepartments.Add(facultyDepartment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFacultyDepartmentDTO", new { id = facultyDepartmentDTO.Id }, facultyDepartmentDTO);
        }

        // DELETE: api/FacultyDepartment/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FacultyDepartment>> DeleteFacultyDepartmentDTO(int id)
        {
            var facultyDepartmentDTO = await _context.FacultyDepartments.FindAsync(id);
            if (facultyDepartmentDTO == null)
            {
                return NotFound();
            }

            _context.FacultyDepartments.Remove(facultyDepartmentDTO);
            await _context.SaveChangesAsync();

            return facultyDepartmentDTO;
        }

        private bool FacultyDepartmentDTOExists(int id)
        {
            return _context.FacultyDepartmentDTO.Any(e => e.Id == id);
        }
    }
}
