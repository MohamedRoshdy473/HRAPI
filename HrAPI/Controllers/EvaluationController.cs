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
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HrAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EvaluationController(ApplicationDbContext context)
        {
            _context = context;
        }
        private Employee CurrentEmployee()
        {
            var CurrentEmp = new Employee();
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var lstUsers = _context.Employees.Where(e => e.Email == email).ToList();
            if (lstUsers.Count > 0)
            {
                CurrentEmp = lstUsers[0];
            }
            return CurrentEmp;
        }
        // GET: api/Evaluation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluationDTO>>> GetEvaluationDTO()
        {
            var evaluation = _context.Evaluation.Select(E => new EvaluationDTO
            {
                ID = E.ID,
                Note = E.Note,
                EmployeeID = E.EmployeeID,
                EmployeeName = E.Employee.Name,
                EvaluationDate = E.EvaluationDate,
                EvaluationDegreee = (E.EvaluationDegreee * 100) / 10,
                EvaluationProfessionID = E.EvaluationProfessionID,
                ProfessionName = E.evaluationProfession.profession.Name,
                EvaluationTypeName = E.evaluationProfession.evaluationType.Name,
            });
            return await evaluation.ToListAsync();
        }
        [Route("GetEvaluationByManager")]
        public async Task<ActionResult<IEnumerable<EvaluationDTO>>> GetEvaluationByManager()
        {
            var evaluation = _context.Evaluation.Where(ex => ex.Employee.Profession.ManagerID == CurrentEmployee().ID).Select(E => new EvaluationDTO
            {
                ID = E.ID,
                Note = E.Note,
                EmployeeID = E.EmployeeID,
                EmployeeName = E.Employee.Name,
                EvaluationDate = E.EvaluationDate,
                EvaluationDegreee = (E.EvaluationDegreee * 100) / 10,
                EvaluationProfessionID = E.EvaluationProfessionID,
                ProfessionName = E.evaluationProfession.profession.Name,
                EvaluationTypeName = E.evaluationProfession.evaluationType.Name,
            });
            return await evaluation.ToListAsync();
        }
        [Route("GetEvaluationForEmployee/{EmployeeId}")]
        public async Task<ActionResult<IEnumerable<EvaluationDTO>>> GetEvaluationForEmployee(int EmployeeId)
        {
            var evaluation = _context.Evaluation.Where(e=>e.EmployeeID== EmployeeId).Select(E => new EvaluationDTO
            {
                ID = E.ID,
                Note = E.Note,
                EmployeeID = E.EmployeeID,
                EmployeeName = E.Employee.Name,
                EvaluationDate = E.EvaluationDate,
                EvaluationDegreee = (E.EvaluationDegreee * 100) / 10,
                EvaluationProfessionID = E.EvaluationProfessionID,
                ProfessionName = E.evaluationProfession.profession.Name,
                EvaluationTypeName = E.evaluationProfession.evaluationType.Name,
            });
            return await evaluation.ToListAsync();
        }
        // GET: api/Evaluation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluationDTO>> GetEvaluationDTO(int id)
        {
            //= await _context.EvaluationDTO.FindAsync(id);

            var evaluation = await _context.Evaluation
            .Include(e => e.Employee).Include(e => e.evaluationProfession)
            .Include(e => e.evaluationProfession.profession)
            .Include(e => e.evaluationProfession.evaluationType)
            .FirstOrDefaultAsync(e => e.ID == id);
            var evaluationDTO = new EvaluationDTO
            {
                ID = evaluation.ID,
                Note = evaluation.Note,
                EmployeeID = evaluation.EmployeeID,
                EmployeeName = evaluation.Employee.Name,
                EvaluationDate = evaluation.EvaluationDate,
                EvaluationDegreee = evaluation.EvaluationDegreee,
                EvaluationProfessionID = _context.EvaluationProfessions.Where(e => e.EvaluationTypeID == evaluation.evaluationProfession.EvaluationTypeID && e.ProfessionID == evaluation.evaluationProfession.ProfessionID).FirstOrDefault().ID,
                ProfessionID = _context.EvaluationProfessions.Where(e => e.ProfessionID == evaluation.evaluationProfession.ProfessionID).FirstOrDefault().profession.ID,
                ProfessionName = _context.EvaluationProfessions.Where(e => e.ProfessionID == evaluation.evaluationProfession.ProfessionID).FirstOrDefault().profession.Name,
                EvaluationTypeID = _context.EvaluationProfessions.Where(e => e.EvaluationTypeID == evaluation.evaluationProfession.EvaluationTypeID).FirstOrDefault().evaluationType.ID,
                EvaluationTypeName = _context.EvaluationProfessions.Where(e => e.EvaluationTypeID == evaluation.evaluationProfession.EvaluationTypeID).FirstOrDefault().evaluationType.Name,

            };
            if (evaluationDTO == null)
            {
                return NotFound();
            }

            return evaluationDTO;
        }
        [HttpGet]
        [Route("GetEvaluationByEmployeeID/{employeeID}")]
        public ActionResult<IEnumerable<EvaluationDTO>> GetEvaluationByEmployeeID(int employeeID)
        {
            //= await _context.EvaluationDTO.FindAsync(id);

            var evaluation = _context.Evaluation
            .Include(e => e.Employee).Include(e => e.evaluationProfession)
            .Include(e => e.evaluationProfession.profession)
            .Include(e => e.evaluationProfession.evaluationType)
            .Where(e => e.EmployeeID == employeeID).Select(eval => new EvaluationDTO
            {
                ID = eval.ID,
                Note = eval.Note,
                EmployeeID = eval.EmployeeID,
                EmployeeName = eval.Employee.Name,
                EvaluationDate = eval.EvaluationDate,
                EvaluationDegreee = eval.EvaluationDegreee,
                EvaluationProfessionID = eval.EvaluationProfessionID,
                ProfessionName = _context.EvaluationProfessions.Where(e => e.ProfessionID == eval.evaluationProfession.ProfessionID).FirstOrDefault().profession.Name,
                EvaluationTypeName = _context.EvaluationProfessions.Where(e => e.EvaluationTypeID == eval.evaluationProfession.EvaluationTypeID).FirstOrDefault().evaluationType.Name,
            }).ToList();
            if (evaluation == null)
            {
                return NotFound();
            }

            return evaluation;
        }

        [HttpGet]
        [Route("GetEvaluationObjByEmployeeID/{employeeID}")]
        public ActionResult<EvaluationDTO> GetEvaluationObjByEmployeeID(int employeeID)
        {
            //= await _context.EvaluationDTO.FindAsync(id);

            //var evaluation =  _context.Evaluation
            //.Include(e => e.Employee).Include(e => e.evaluationProfession)
            //.Include(e => e.evaluationProfession.profession)
            //.Include(e => e.evaluationProfession.evaluationType)
            //.Where(e => e.EmployeeID == employeeID).Select(eval=> new EvaluationDTO
            //{
            //    ID = eval.ID,
            //    EvaluationProfessionID = eval.EvaluationProfessionID,                
            //    ProfessionName = _context.EvaluationProfessions.Where(e => e.ProfessionID == eval.evaluationProfession.ProfessionID).FirstOrDefault().profession.Name,
            //    ProfessionID = _context.EvaluationProfessions.Where(e => e.ProfessionID == eval.evaluationProfession.ProfessionID).FirstOrDefault().profession.ID,
            //    EvaluationTypeName = _context.EvaluationProfessions.Where(e => e.EvaluationTypeID == eval.evaluationProfession.EvaluationTypeID).FirstOrDefault().evaluationType.Name,
            //}).FirstOrDefault();

            var evaluation = (from ep in _context.EvaluationProfessions
                              join et in _context.EvaluationTypes on ep.EvaluationTypeID equals et.ID
                              join pro in _context.Professions on ep.ProfessionID equals pro.ID
                              join emp in _context.Employees on ep.ProfessionID equals emp.ProfessionID
                              where emp.ID == employeeID
                              select new EvaluationDTO
                              {
                                  EvaluationProfessionID = ep.ID,
                                  ProfessionName = pro.Name,
                                  ProfessionID = pro.ID,
                                  EvaluationTypeName = et.Name

                              }).FirstOrDefault();
            return evaluation;
        }
        // PUT: api/Evaluation/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvaluationDTO(int id, EvaluationViewModel evaluationViewModel)
        {
            if (id != evaluationViewModel.ID)
            {
                return BadRequest();
            }
            Evaluation evaluation = new Evaluation
            {
                ID = evaluationViewModel.ID,
                Note = evaluationViewModel.Note,
                EmployeeID = evaluationViewModel.EmployeeID,
                EvaluationDate = evaluationViewModel.EvaluationDate,
                EvaluationDegreee = evaluationViewModel.EvaluationDegreee,
                EvaluationProfessionID = _context.EvaluationProfessions.Where(e => e.EvaluationTypeID == evaluationViewModel.EvaluationTypeID && e.ProfessionID == evaluationViewModel.ProfessionID).FirstOrDefault().ID,
                //evaluationViewModel.EvaluationProfessionID
            };
            _context.Entry(evaluation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvaluationDTOExists(id))
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

        // POST: api/Evaluation
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EvaluationDTO>> PostEvaluationDTO(EvaluationViewModel evaluationViewModel)
        {
            Evaluation evaluation = new Evaluation
            {
                ID = evaluationViewModel.ID,
                Note = evaluationViewModel.Note,
                EmployeeID = evaluationViewModel.EmployeeID,
                EvaluationDate = evaluationViewModel.EvaluationDate,
                EvaluationDegreee = evaluationViewModel.EvaluationDegreee,
                EvaluationProfessionID = _context.EvaluationProfessions.Where(e => e.EvaluationTypeID == evaluationViewModel.EvaluationTypeID && e.ProfessionID == evaluationViewModel.ProfessionID).FirstOrDefault().ID,
            };
            _context.Evaluation.Add(evaluation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvaluationDTO", new { id = evaluationViewModel.ID }, evaluationViewModel);
        }

        // DELETE: api/Evaluation/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Evaluation>> DeleteEvaluationDTO(int id)
        {
            var evaluationDTO = await _context.Evaluation.FindAsync(id);
            if (evaluationDTO == null)
            {
                return NotFound();
            }

            _context.Evaluation.Remove(evaluationDTO);
            await _context.SaveChangesAsync();

            return evaluationDTO;
        }

        private bool EvaluationDTOExists(int id)
        {
            return _context.Evaluation.Any(e => e.ID == id);
        }
    }
}
