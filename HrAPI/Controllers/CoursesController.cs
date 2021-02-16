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
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoursesDTO>>> GetCoursesDTO()
        {
            return await _context.courses.Include(c => c.TrainingType).Select(c => new CoursesDTO
            {
                ID = c.ID,
                CourseName = c.CourseName,
                TrainingTypeID = c.TrainingTypeID,
                TrainingTypeName = c.TrainingType.TrainingTypeName
            }).ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CoursesDTO>> GetCoursesDTO(int id)
        {
            var courses = await _context.courses.Include(c => c.TrainingType)
                .FirstOrDefaultAsync(c => c.ID == id);
            var coursesDTO = new CoursesDTO
            {
                ID = courses.ID,
                CourseName = courses.CourseName,
                TrainingTypeID = courses.TrainingTypeID,
                TrainingTypeName = courses.TrainingType.TrainingTypeName
            };

            if (coursesDTO == null)
            {
                return NotFound();
            }

            return coursesDTO;
        }

        [HttpGet]
        [Route("GetCoursesByTrainingTypeID/{trainingTypeID}")]
        public async Task<ActionResult<IEnumerable<CoursesDTO>>> GetCoursesByTrainingTypeID(int trainingTypeID)
        {
            var courses = await _context.courses.Include(c => c.TrainingType)
                .Where(c => c.TrainingTypeID== trainingTypeID).Select(crs=>new CoursesDTO
            {
                ID = crs.ID,
                CourseName = crs.CourseName,
                TrainingTypeID = crs.TrainingTypeID,
                TrainingTypeName = crs.TrainingType.TrainingTypeName
            }).ToListAsync();

            if (courses == null)
            {
                return NotFound();
            }

            return courses;
        }
        // PUT: api/Courses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoursesDTO(int id, CoursesDTO coursesDTO)
        {
            if (id != coursesDTO.ID)
            {
                return BadRequest();
            }
            var course = new Courses
            {
                ID = coursesDTO.ID,
                CourseName = coursesDTO.CourseName,
                TrainingTypeID = coursesDTO.TrainingTypeID,
            };
            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoursesDTOExists(id))
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

        // POST: api/Courses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CoursesDTO>> PostCoursesDTO(CoursesDTO coursesDTO)
        {
            var course = new Courses
            {
                ID = coursesDTO.ID,
                CourseName = coursesDTO.CourseName,
                TrainingTypeID = coursesDTO.TrainingTypeID,
            };
            _context.courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoursesDTO", new { id = coursesDTO.ID }, coursesDTO);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Courses>> DeleteCoursesDTO(int id)
        {
            var coursesDTO = await _context.courses.FindAsync(id);
            if (coursesDTO == null)
            {
                return NotFound();
            }

            _context.courses.Remove(coursesDTO);
            await _context.SaveChangesAsync();

            return coursesDTO;
        }

        private bool CoursesDTOExists(int id)
        {
            return _context.courses.Any(e => e.ID == id);
        }
    }
}
