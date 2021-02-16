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
    public class TrainingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrainingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Training
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> GetTrainingDTO()
        {
            var trainingDTO = _context.Trainings.Select(train => new TrainingDTO
            {
                ID = train.ID,
                StartDate = train.StartDate,
                EndDate = train.EndDate,
                Certified = train.Certified,
                TrainingPlace = train.TrainingPlace,
                EmployeeID = train.EmployeeID,
                EmployeeName = train.Employee.Name,
                InstructorID = train.InstructorID,
                InstructorName = train.Instructor.InstructorName,
                TrainingProfessionID = _context.TrainingProfessions.Where(e => e.ID == train.TrainingProfessionID).FirstOrDefault().ID,
                ProfessionID = _context.TrainingProfessions.Where(e => e.ProfessionID == train.TrainingProfessions.ProfessionID).FirstOrDefault().Profession.ID,
                ProfessionName = _context.TrainingProfessions.Where(e => e.ProfessionID == train.TrainingProfessions.ProfessionID).FirstOrDefault().Profession.Name,
                TrainingTypeID = _context.TrainingProfessions.Where(e => e.Courses.TrainingTypeID == train.TrainingProfessions.Courses.TrainingTypeID).FirstOrDefault().Courses.TrainingType.ID,
                TrainingTypeName = _context.TrainingProfessions.Where(e => e.Courses.TrainingTypeID == train.TrainingProfessions.Courses.TrainingTypeID).FirstOrDefault().Courses.TrainingType.TrainingTypeName,
                CourseID = _context.TrainingProfessions.Where(e => e.Courses.ID == train.TrainingProfessions.CourseID).FirstOrDefault().Courses.ID,
                CourseName = _context.TrainingProfessions.Where(e => e.Courses.ID == train.TrainingProfessions.CourseID).FirstOrDefault().Courses.CourseName,
            }).ToListAsync();

            return await trainingDTO;
        }

        // GET: api/Training/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingDTO>> GetTrainingDTO(int id)
        {
           var typeName= (from tp in _context.TrainingProfessions
             join crse in _context.courses on tp.CourseID equals crse.ID
             join type in _context.TrainingTypes on crse.TrainingTypeID equals type.ID
             join training in _context.Trainings on tp.ID equals training.TrainingProfessionID
             where training.ID == id
             select type);

            var train = await _context.Trainings
             .Include(e => e.Employee).Include(e => e.TrainingProfessions)
            .Include(e => e.TrainingProfessions.Profession)
            .Include(e => e.TrainingProfessions.Courses)
            //.Include(e => e.TrainingProfessions.Courses.TrainingType)
            .Include(e => e.Instructor)
                .FirstOrDefaultAsync(T => T.ID == id);
            var trainingDTO = new TrainingDTO
            {

                ID = train.ID,
                StartDate = train.StartDate,
                EndDate = train.EndDate,
                Certified = train.Certified,
                TrainingPlace = train.TrainingPlace,
                EmployeeID = train.EmployeeID,
                EmployeeName = train.Employee.Name,
                InstructorID = train.InstructorID,
                InstructorName = train.Instructor.InstructorName,
                TrainingProfessionID = _context.TrainingProfessions.Where(e => e.ID == train.TrainingProfessionID).FirstOrDefault().ID,
                ProfessionID = _context.TrainingProfessions.Where(e => e.ProfessionID == train.TrainingProfessions.ProfessionID).FirstOrDefault().Profession.ID,
                ProfessionName = _context.TrainingProfessions.Where(e => e.ProfessionID == train.TrainingProfessions.ProfessionID).FirstOrDefault().Profession.Name,
                TrainingTypeID = (from tp in _context.TrainingProfessions
                                 join crse in _context.courses on tp.CourseID equals crse.ID
                                 join type in _context.TrainingTypes on crse.TrainingTypeID equals type.ID
                                 join training in _context.Trainings on tp.ID equals training.TrainingProfessionID
                                 where training.ID == id
                                 select type).FirstOrDefault().ID,
                TrainingTypeName = (from tp in _context.TrainingProfessions
                                    join crse in _context.courses on tp.CourseID equals crse.ID
                                    join type in _context.TrainingTypes on crse.TrainingTypeID equals type.ID
                                    join training in _context.Trainings on tp.ID equals training.TrainingProfessionID
                                    where training.ID == id
                                    select type).FirstOrDefault().TrainingTypeName,
                CourseID = _context.TrainingProfessions.Where(e => e.Courses.ID == train.TrainingProfessions.CourseID).FirstOrDefault().Courses.ID,
                CourseName = _context.TrainingProfessions.Where(e => e.Courses.ID == train.TrainingProfessions.CourseID).FirstOrDefault().Courses.CourseName,
            };
            if (trainingDTO == null)
            {
                return NotFound();
            }

            return trainingDTO;
        }
        //[HttpGet]
        //[Route("GetTrainingByprofessionID/{professionID}")]
        //public ActionResult<IEnumerable<TrainingProfessionsDTO>> GetTrainingByprofessionID(int professionID)
        //{
        //    var training = _context.TrainingProfessions
        //    .Include(e => e.Profession.Employees)
        //    .Include(e => e.Courses)
        //    .Include(e => e.Courses.TrainingType)
        //    .Include(e=>e.Profession.Employees)
        //    .Where(e => e.Profession.ID == professionID).Select(train => new TrainingProfessionsDTO
        //    {
        //        ID = _context.TrainingProfessions.Where(e => e.ID == train.ID).FirstOrDefault().ID,
        //        EmployeeID = (from prof in _context.Professions
        //                     join emp in _context.Employees on prof.ID equals emp.ProfessionID 
        //                     where emp.ProfessionID==professionID select emp).FirstOrDefault().ID,
        //        EmployeeName = (from prof in _context.Professions
        //                      join emp in _context.Employees on prof.ID equals emp.ProfessionID
        //                      where emp.ProfessionID == professionID
        //                      select emp).FirstOrDefault().Name,
        //        ProfessionID = _context.TrainingProfessions.Where(e => e.ProfessionID == train.ProfessionID).FirstOrDefault().Profession.ID,
        //        TrainingTypeID = _context.TrainingProfessions.Where(e => e.Courses.TrainingTypeID == train.Courses.TrainingTypeID).FirstOrDefault().Courses.TrainingType.ID,
        //        TrainingTypeName = _context.TrainingProfessions.Where(e => e.Courses.TrainingTypeID == train.Courses.TrainingTypeID).FirstOrDefault().Courses.TrainingType.TrainingTypeName,
        //        CourseID = _context.TrainingProfessions.Where(e => e.Courses.ID == train.CourseID).FirstOrDefault().CourseID,
        //        CourseName = _context.TrainingProfessions.Where(e => e.Courses.ID == train.CourseID).FirstOrDefault().Courses.CourseName,
        //    }).ToList();
        //    if (training == null)
        //    {
        //        return NotFound();
        //    }
        //    return training;
        //}
        [HttpGet]
        [Route("GetTrainingTypesByprofessionID/{professionID}")]
        public ActionResult<IEnumerable<TrainingProfessionsDTO>> GetTrainingTypesByprofessionID(int professionID)
        {
            var trainingTypes = (from tp in _context.TrainingProfessions
                                 join crse in _context.courses on tp.CourseID equals crse.ID
                                 join traintype in _context.TrainingTypes on crse.TrainingTypeID equals traintype.ID
                                 where tp.ProfessionID == professionID
                                 select traintype).ToList().GroupBy(a => a.ID).Select(traintype => new TrainingProfessionsDTO
                                 {
                                     TrainingTypeName = traintype.FirstOrDefault().TrainingTypeName,
                                     TrainingTypeID = traintype.FirstOrDefault().ID
                                 }).ToList();

            if (trainingTypes == null)
            {
                return NotFound();
            }

            return trainingTypes;
        }

        [HttpGet]
        [Route("GetCoursesByTrainingTypeID/{trainingTypeID}")]
        public ActionResult<IEnumerable<TrainingProfessionsDTO>> GetCoursesByTrainingTypeID(int trainingTypeID)
        {
            var trainingTypes = (from tp in _context.TrainingProfessions
                                 join crse in _context.courses on tp.CourseID equals crse.ID
                                 join traintype in _context.TrainingTypes on crse.TrainingTypeID equals traintype.ID
                                 where tp.Courses.TrainingTypeID == trainingTypeID
                                 select crse).ToList().GroupBy(a => a.ID).Select(traintype => new TrainingProfessionsDTO
                                 {
                                     CourseName = traintype.FirstOrDefault().CourseName,
                                     CourseID = traintype.FirstOrDefault().ID
                                 }).ToList();

            if (trainingTypes == null)
            {
                return NotFound();
            }

            return trainingTypes;
        }





        [Route("Certified/{id}")]
        public ActionResult Certified(int id)
        {
            var certified = _context.Trainings.Find(id);

            if (certified == null)
            {
                return NotFound();
            }
            var crsName = (from tp in _context.TrainingProfessions
                           join crse in _context.courses on tp.CourseID equals crse.ID
                           join train in _context.Trainings on tp.ID equals train.TrainingProfessionID
                           where train.ID == certified.ID
                           select crse);



            Certificates certificates = new Certificates()
            {
                Certificate = crsName.FirstOrDefault().CourseName,
                EmployeeID = certified.EmployeeID,
                CertificateDate = certified.EndDate,
                CertificatePlace = certified.TrainingPlace
            };
            certified.Certified = "Certified";
            _context.Certificates.Add(certificates);
            _context.SaveChanges();
            return Ok();
        }

        [Route("Certified")]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> GetCertified()
        {
            return await _context.Trainings.Where(train => train.Certified == "Certified").Select(train => new TrainingDTO
            {

                ID = train.ID,
                StartDate = train.StartDate,
                EndDate = train.EndDate,
                Certified = train.Certified,
                TrainingPlace = train.TrainingPlace,
                EmployeeID = train.EmployeeID,
                EmployeeName = train.Employee.Name,
                InstructorID = train.InstructorID,
                InstructorName = train.Instructor.InstructorName,
                TrainingProfessionID = _context.TrainingProfessions.Where(e => e.ID == train.TrainingProfessionID).FirstOrDefault().ID,
                ProfessionID = _context.TrainingProfessions.Where(e => e.ProfessionID == train.TrainingProfessions.ProfessionID).FirstOrDefault().Profession.ID,
                ProfessionName = _context.TrainingProfessions.Where(e => e.ProfessionID == train.TrainingProfessions.ProfessionID).FirstOrDefault().Profession.Name,
                TrainingTypeID = _context.TrainingProfessions.Where(e => e.Courses.TrainingTypeID == train.TrainingProfessions.Courses.TrainingTypeID).FirstOrDefault().Courses.TrainingType.ID,
                TrainingTypeName = _context.TrainingProfessions.Where(e => e.Courses.TrainingTypeID == train.TrainingProfessions.Courses.TrainingTypeID).FirstOrDefault().Courses.TrainingType.TrainingTypeName,
                CourseID = _context.TrainingProfessions.Where(e => e.Courses.ID == train.TrainingProfessions.CourseID).FirstOrDefault().Courses.ID,
                CourseName = _context.TrainingProfessions.Where(e => e.Courses.ID == train.TrainingProfessions.CourseID).FirstOrDefault().Courses.CourseName,
            }).ToListAsync();
        }
        [Route("Pending")]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> GetPending()
        {
            return await _context.Trainings.Where(train => train.Certified == "Pending").Select(train => new TrainingDTO
            {

                ID = train.ID,
                StartDate = train.StartDate,
                EndDate = train.EndDate,
                Certified = train.Certified,
                TrainingPlace = train.TrainingPlace,
                EmployeeID = train.EmployeeID,
                EmployeeName = train.Employee.Name,
                InstructorID = train.InstructorID,
                InstructorName = train.Instructor.InstructorName,
                TrainingProfessionID = _context.TrainingProfessions.Where(e => e.ID == train.TrainingProfessionID).FirstOrDefault().ID,
                ProfessionID = _context.TrainingProfessions.Where(e => e.ProfessionID == train.TrainingProfessions.ProfessionID).FirstOrDefault().Profession.ID,
                ProfessionName = _context.TrainingProfessions.Where(e => e.ProfessionID == train.TrainingProfessions.ProfessionID).FirstOrDefault().Profession.Name,
                TrainingTypeID = _context.TrainingProfessions.Where(e => e.Courses.TrainingTypeID == train.TrainingProfessions.Courses.TrainingTypeID).FirstOrDefault().Courses.TrainingType.ID,
                TrainingTypeName = _context.TrainingProfessions.Where(e => e.Courses.TrainingTypeID == train.TrainingProfessions.Courses.TrainingTypeID).FirstOrDefault().Courses.TrainingType.TrainingTypeName,
                CourseID = _context.TrainingProfessions.Where(e => e.Courses.ID == train.TrainingProfessions.CourseID).FirstOrDefault().Courses.ID,
                CourseName = _context.TrainingProfessions.Where(e => e.Courses.ID == train.TrainingProfessions.CourseID).FirstOrDefault().Courses.CourseName,
            }).ToListAsync();
        }


        // PUT: api/Training/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingDTO(int id, TrainingDTO trainingDTO)
        {
            var trainProfId = 0;
            var lstTrainingProfessions = _context.TrainingProfessions.Where(e => e.CourseID == trainingDTO.CourseID
                                                              && e.ProfessionID == trainingDTO.ProfessionID).ToList();

            if (lstTrainingProfessions.Count > 0)
            {
                TrainingProfessions trainingProfessionObj = lstTrainingProfessions[0];
                trainProfId = trainingProfessionObj.ID;
            }
            if (id != trainingDTO.ID)
            {
                return BadRequest();
            }
            Training train = new Training
            {
                ID = trainingDTO.ID,
                StartDate = trainingDTO.StartDate,
                EndDate = trainingDTO.EndDate,
                Certified = trainingDTO.Certified,
                TrainingPlace = trainingDTO.TrainingPlace,
                EmployeeID = trainingDTO.EmployeeID,
                InstructorID = trainingDTO.InstructorID,
                TrainingProfessionID = trainProfId
            };
            _context.Entry(train).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingDTOExists(id))
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

        // POST: api/Training
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TrainingDTO>> PostTrainingDTO(TrainingDTO trainingDTO)
        {
            var trainProfId = 0;
            var lstTrainingProfessions = _context.TrainingProfessions.Where(e => e.CourseID == trainingDTO.CourseID
                                                              && e.ProfessionID == trainingDTO.ProfessionID).ToList();

            if (lstTrainingProfessions.Count > 0)
            {
                TrainingProfessions trainingProfessionObj = lstTrainingProfessions[0];
                trainProfId = trainingProfessionObj.ID;
            }
            Training train = new Training
            {
                // ID = trainingDTO.ID,
                StartDate = trainingDTO.StartDate,
                EndDate = trainingDTO.EndDate,
                Certified = trainingDTO.Certified,
                TrainingPlace = trainingDTO.TrainingPlace,
                EmployeeID = trainingDTO.EmployeeID,
                InstructorID = trainingDTO.InstructorID,
                TrainingProfessionID = trainProfId
            };
            _context.Trainings.Add(train);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainingDTO", new { id = trainingDTO.ID }, trainingDTO);
        }

        // DELETE: api/Training/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Training>> DeleteTrainingDTO(int id)
        {
            var trainingDTO = await _context.Trainings.FindAsync(id);
            if (trainingDTO == null)
            {
                return NotFound();
            }

            _context.Trainings.Remove(trainingDTO);
            await _context.SaveChangesAsync();

            return trainingDTO;
        }

        private bool TrainingDTOExists(int id)
        {
            return _context.Trainings.Any(e => e.ID == id);
        }
    }
}
