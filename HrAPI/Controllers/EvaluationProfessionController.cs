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
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace HrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationProfessionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EvaluationProfessionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EvaluationProfession
        [HttpGet]
        public IEnumerable<EvaluationProfessionDTO> GetEvaluationProfessionDTO()
        {
            var evaluationProfession = _context.EvaluationProfessions.Select(E => new EvaluationProfessionDTO
            {
                ID =E.ID,
                ProfessionName=E.profession.Name,
                EvaluationTypeName=E.evaluationType.Name,
                ProfessionID=E.ProfessionID,
                EvaluationTypeID=E.EvaluationTypeID

              }).ToList();
            return evaluationProfession;
        }
        [HttpGet]
        [Route("GetEvaluationByProfession/{ProfessionId}")]
        public IEnumerable<EvaluationProfessionDTO> GetEvaluationByProfessionId(int ProfessionId)
        {
            var evaluationByProfession = _context.EvaluationProfessions
                .Where(e => e.ProfessionID == ProfessionId)
                .Select(E => new EvaluationProfessionDTO {
                ProfessionName = E.profession.Name,
                EvaluationTypeName=E.evaluationType.Name,
                ProfessionID = E.ProfessionID,

                    //EvaluationTypeName=E.evaluationType.Name
                }).ToList();
            return evaluationByProfession;
        }
        [HttpGet]
        [Route("GetEvaluationTypeByProfession/{ProfessionId}")]
        public IEnumerable<EvaluationProfessionDTO> GetEvaluationTypeByProfessionId(int ProfessionId)
        {
            var evaluationTypeByProfession = _context.EvaluationProfessions
                .Where(e => e.ProfessionID == ProfessionId)
                .Select(e => new EvaluationProfessionDTO
                {
                    EvaluationTypeName = e.evaluationType.Name,
                    EvaluationTypeID=e.EvaluationTypeID
                }).ToList();
            return evaluationTypeByProfession;
        }


        [HttpGet]
        [Route("GetEvaluationTypeNotByProfession/{ProfessionId}")]
        public IEnumerable<EvaluationProfessionDTO> GetEvaluationTypeNotByProfession(int ProfessionId)
        {
            var listEvaluationTypesIds = _context.EvaluationTypes.ToList().Select(a=>a.ID).ToList();

            var listEvaluationProfessionIds = _context.EvaluationProfessions.Where(a=>a.ProfessionID == ProfessionId).ToList().Select(a => a.EvaluationTypeID).ToList();

           var remainIds = listEvaluationTypesIds.Except(listEvaluationProfessionIds);

            var evaluationTypeByProfession = _context.EvaluationTypes
                .Where(a => remainIds.Contains(a.ID))
                .Select(e=>new EvaluationProfessionDTO {EvaluationTypeID=e.ID,EvaluationTypeName=e.Name }).ToList();
            return evaluationTypeByProfession;
            //var evaluationTypeByProfession = (from  evalProf in _context.EvaluationProfessions //on prof.ID equals evalProf.ProfessionID
            //                                  join type in _context.EvaluationTypes on evalProf.EvaluationTypeID equals type.ID

            //                                  where
            //                                  evalProf.ProfessionID == ProfessionId &&
            //                                   remainIds.Contains(type.ID)
            //                                  select new EvaluationProfessionDTO
            //                                  {
            //                                      EvaluationTypeName = evalProf.evaluationType.Name,
            //                                      EvaluationTypeID = evalProf.EvaluationTypeID
            //                                  }).ToList();

            //var evaluationTypeByNotProfession = _context.EvaluationProfessions
            //    .Where(e => e.ProfessionID == ProfessionId)
            //    .Select(e => new EvaluationProfessionDTO
            //    {
            //        EvaluationTypeName = e.evaluationType.Name,
            //        EvaluationTypeID = e.EvaluationTypeID
            //    }).ToList();

        }
        // GET: api/EvaluationProfession/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluationProfessionDTO>> GetEvaluationProfessionDTO(int id)
        {
            var evaluationProfession = await _context.EvaluationProfessions
                .Include(e=>e.profession).Include(e=>e.evaluationType)
                .FirstOrDefaultAsync(e=>e.ID==id);

            var evaluationProfessionDTO = new EvaluationProfessionDTO
            {
                ID = evaluationProfession.ID,
                ProfessionName = evaluationProfession.profession.Name,
                EvaluationTypeName = evaluationProfession.evaluationType.Name,
                ProfessionID = evaluationProfession.ProfessionID,
                EvaluationTypeID = evaluationProfession.EvaluationTypeID
            };
            if (evaluationProfession == null)
            {
                return NotFound();
            }

            return evaluationProfessionDTO;
        }

        // PUT: api/EvaluationProfession/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvaluationProfessionDTO(int id, EvaluationProfessionViewModel evaluationProfessionViewModel)
        {
            if (id != evaluationProfessionViewModel.ID)
            {
                return BadRequest();
            }
            EvaluationProfession evaluationProfession = new EvaluationProfession
            {
                ID = evaluationProfessionViewModel.ID,
                ProfessionID=evaluationProfessionViewModel.ProfessionID,
                EvaluationTypeID=evaluationProfessionViewModel.EvaluationTypeID

            };
            _context.Entry(evaluationProfession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvaluationProfessionDTOExists(id))
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

        // POST: api/EvaluationProfession
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EvaluationProfessionDTO>> PostEvaluationProfessionDTO(EvaluationProfessionViewModel evaluationProfessionViewModel)
        {
           // var eval = _context.EvaluationProfessions.Where(e=>e.ProfessionID==evaluationProfessionViewModel.ProfessionID).Select(e => e.EvaluationTypeID);
            EvaluationProfession evaluationProfession = new EvaluationProfession
            {
                ID = evaluationProfessionViewModel.ID,
                ProfessionID = evaluationProfessionViewModel.ProfessionID,
                EvaluationTypeID = evaluationProfessionViewModel.EvaluationTypeID
            };
            _context.EvaluationProfessions.Add(evaluationProfession);
            await _context.SaveChangesAsync();
            //try
            //{
            //    if (evaluationProfessionViewModel.EvaluationTypeID.c(eval))
            //    {
            //        return NoContent();
            //    }
            //    else
            //    {
                     
            //    }
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //     return NotFound();
            //}

            return CreatedAtAction("GetEvaluationProfessionDTO", new { id = evaluationProfessionViewModel.ID }, evaluationProfessionViewModel);
        }

        // DELETE: api/EvaluationProfession/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EvaluationProfession>> DeleteEvaluationProfessionDTO(int id)
        {
            var evaluationProfessionDTO = await _context.EvaluationProfessions.FindAsync(id);
            if (evaluationProfessionDTO == null)
            {
                return NotFound();
            }

            _context.EvaluationProfessions.Remove(evaluationProfessionDTO);
            await _context.SaveChangesAsync();

            return evaluationProfessionDTO;
        }

        private bool EvaluationProfessionDTOExists(int id)
        {
            return _context.EvaluationProfessions.Any(e => e.ID == id);
        }
    }
}
