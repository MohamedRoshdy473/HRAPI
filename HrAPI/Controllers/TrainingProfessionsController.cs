using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HrAPI.DTO;
using HrAPI.Models;
using HrAPI.ViewModels;

namespace HrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingProfessionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrainingProfessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TrainingProfessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingProfessionsDTO>>> GetTrainingProfessionsDTO()
        {
            return await _context.TrainingProfessions.Select(trainingProfession => new TrainingProfessionsDTO
            {
                ID = trainingProfession.ID,
                CourseID = trainingProfession.CourseID,
                CourseName = trainingProfession.Courses.CourseName,
                ProfessionID = trainingProfession.ProfessionID,
                ProfessionName = trainingProfession.Profession.Name,
                TrainingTypeID = trainingProfession.Courses.TrainingTypeID,
                TrainingTypeName = trainingProfession.Courses.TrainingType.TrainingTypeName
            }
            ).ToListAsync();
        }

        // GET: api/TrainingProfessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingProfessionsDTO>> GetTrainingProfessionsDTO(int id)
        {
            var trainingProfession = await _context.TrainingProfessions.Include(c=>c.Profession).Include(t => t.Courses.TrainingType).FirstOrDefaultAsync(t => t.ID == id);
            var trainingProfessionsDTO = new TrainingProfessionsDTO
            {
                ID = trainingProfession.ID,
                CourseID = trainingProfession.CourseID,
                CourseName=trainingProfession.Courses.CourseName,
                ProfessionID = trainingProfession.ProfessionID,
                ProfessionName = trainingProfession.Profession.Name,
                TrainingTypeName = _context.TrainingProfessions.Where(e => e.Courses.TrainingTypeID == trainingProfession.Courses.TrainingTypeID).FirstOrDefault().Courses.TrainingType.TrainingTypeName,
                TrainingTypeID = _context.TrainingProfessions.Where(e => e.Courses.TrainingTypeID == trainingProfession.Courses.TrainingTypeID).FirstOrDefault().Courses.TrainingTypeID,
            };
            if (trainingProfessionsDTO == null)
            {
                return NotFound();
            }

            return trainingProfessionsDTO;
        }

        [HttpGet]
        [Route("GetTrainingAndCourseByProfessionId/{ProfessionId}")]
        public IEnumerable<TrainingProfessionsDTO> GetTrainingAndCourseByProfessionId(int ProfessionId)
        {
            var trainingProfession = _context.TrainingProfessions
                .Where(e => e.ProfessionID == ProfessionId)
                .Select(trainingProfession => new TrainingProfessionsDTO
                {
                    ID = trainingProfession.ID,
                    CourseID = trainingProfession.CourseID,
                    CourseName = trainingProfession.Courses.CourseName,
                    ProfessionID = trainingProfession.ProfessionID,
                    ProfessionName = trainingProfession.Profession.Name,
                    TrainingTypeID = trainingProfession.Courses.TrainingTypeID,
                    TrainingTypeName = trainingProfession.Courses.TrainingType.TrainingTypeName
                }).ToList();
            return trainingProfession;
        }


        [HttpGet]
        [Route("GetTrainingAndCourseNotByProfessionId/{ProfessionId}")]
        public IEnumerable<TrainingProfessionsDTO> GetTrainingAndCourseNotByProfessionId(int ProfessionId)
        {
            var listTrainingType = _context.TrainingTypes.ToList().Select(c => c.ID).ToList();

            var listTrainingProfession = _context.TrainingProfessions.Where(c => c.ProfessionID == ProfessionId)
                .ToList().Select(c => c.CourseID).ToList();

            var remainId = listTrainingType.Except(listTrainingProfession);

            var TrainingAndTypeByProfession = _context.courses.Where(c => remainId.Contains(c.TrainingTypeID))

                .Select(
                c => new TrainingProfessionsDTO
                {
                    CourseID = c.ID,
                    CourseName = c.CourseName,
                    TrainingTypeID = c.TrainingType.ID,
                    TrainingTypeName = c.TrainingType.TrainingTypeName
                }).ToList().GroupBy(c => c.TrainingTypeID);
            List<TrainingProfessionsDTO> list = new List<TrainingProfessionsDTO>();
            foreach (var item in TrainingAndTypeByProfession)
            {
                TrainingProfessionsDTO trainingObj = new TrainingProfessionsDTO();
                trainingObj.TrainingTypeID = item.FirstOrDefault().TrainingTypeID;
                trainingObj.CourseID = item.FirstOrDefault().CourseID;
                trainingObj.TrainingTypeName = item.FirstOrDefault().TrainingTypeName;
                trainingObj.CourseName = item.FirstOrDefault().CourseName;

                list.Add(trainingObj);
            }
            return list;
        }
        // PUT: api/TrainingProfessions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingProfessionsDTO(int id, TrainingProfessionViewModel trainingProfessionViewModel)
        {
            //Courses courses = new Courses();
            if (id != trainingProfessionViewModel.ID)
            {
                return BadRequest();
            }
            var trainingProfession = new TrainingProfessions
            {
                ID = trainingProfessionViewModel.ID,
                CourseID = trainingProfessionViewModel.CourseID,
                ProfessionID = trainingProfessionViewModel.ProfessionID,
            };
            _context.Entry(trainingProfession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingProfessionsDTOExists(id))
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

        // POST: api/TrainingProfessions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TrainingProfessionsDTO>> PostTrainingProfessionsDTO(TrainingProfessionsDTO trainingProfessionsDTO)
        {
            var trainingProfession = new TrainingProfessions
            {
                ID = trainingProfessionsDTO.ID,
                CourseID = trainingProfessionsDTO.CourseID,
                ProfessionID = trainingProfessionsDTO.ProfessionID

            };
            _context.TrainingProfessions.Add(trainingProfession);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainingProfessionsDTO", new { id = trainingProfessionsDTO.ID }, trainingProfessionsDTO);
        }

        // DELETE: api/TrainingProfessions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TrainingProfessions>> DeleteTrainingProfessionsDTO(int id)
        {
            var trainingProfessionsDTO = await _context.TrainingProfessions.FindAsync(id);
            if (trainingProfessionsDTO == null)
            {
                return NotFound();
            }

            _context.TrainingProfessions.Remove(trainingProfessionsDTO);
            await _context.SaveChangesAsync();

            return trainingProfessionsDTO;
        }

        private bool TrainingProfessionsDTOExists(int id)
        {
            return _context.TrainingProfessions.Any(e => e.ID == id);
        }
    }
}
