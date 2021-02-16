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
    public class FacultyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FacultyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Faculty
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacultyDTO>>> GetFacultyDTO()
        {
            return await _context.Faculties.Select(fac=>new FacultyDTO { 
            Id=fac.Id,
            FacultyName=fac.FacultyName,
            UniversityID=fac.UniversityID,
            UniversityName=fac.University.UniversityName
            }).ToListAsync();
        }

        // GET: api/Faculty/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FacultyDTO>> GetFacultyDTO(int id)
        {
            var fac = await _context.Faculties.Include(f => f.University).FirstOrDefaultAsync(f=>f.Id==id);
            var faculty = new FacultyDTO
            {
                Id = fac.Id,
                FacultyName = fac.FacultyName,
                UniversityID = fac.UniversityID,
                UniversityName = fac.University.UniversityName
            };
            if (fac == null)
            {
                return NotFound();
            }

            return faculty;
        }
        [HttpGet]
        [Route("GetFacultiesByUniversityId/{UniversityId}")]
        public IEnumerable<FacultyDTO> GetFacultiesByUniversityId(int UniversityId)
        {
            var faculty = _context.Faculties.Include(f => f.University).Where(f => f.UniversityID == UniversityId).Select(fac=>new FacultyDTO { 
                Id = fac.Id,
                FacultyName = fac.FacultyName,
                UniversityID = fac.UniversityID,
                UniversityName = fac.University.UniversityName
            }).ToList();
            return faculty;
        }
        // PUT: api/Faculty/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacultyDTO(int id, FacultyDTO facultyDTO)
        {
            if (id != facultyDTO.Id)
            {
                return BadRequest();
            }
            Faculty faculty = new Faculty();
            faculty.Id = facultyDTO.Id;
            faculty.FacultyName = facultyDTO.FacultyName;
            faculty.UniversityID = facultyDTO.UniversityID;
            _context.Entry(faculty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacultyDTOExists(id))
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

        // POST: api/Faculty
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FacultyDTO>> PostFacultyDTO(FacultyDTO facultyDTO)
        {
            Faculty faculty = new Faculty();
            //faculty.Id = facultyDTO.Id;
            faculty.FacultyName = facultyDTO.FacultyName;
            faculty.UniversityID = facultyDTO.UniversityID;
            _context.Faculties.Add(faculty);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFacultyDTO", new { id = facultyDTO.Id }, facultyDTO);
        }

        // DELETE: api/Faculty/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Faculty>> DeleteFacultyDTO(int id)
        {
            var facultyDTO = await _context.Faculties.FindAsync(id);
            if (facultyDTO == null)
            {
                return NotFound();
            }

            _context.Faculties.Remove(facultyDTO);
            await _context.SaveChangesAsync();

            return facultyDTO;
        }

        private bool FacultyDTOExists(int id)
        {
            return _context.FacultyDTO.Any(e => e.Id == id);
        }
    }
}
