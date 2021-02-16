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
    public class CertificatesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CertificatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Certificates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CertificatesDTO>>> GetCertificatesDTO()
        {

            return await _context.Certificates.Select(c => new CertificatesDTO
            {
                ID = c.ID,
                Certificate = c.Certificate,
                EmployeeID = c.EmployeeID,
                EmployeeName = c.Employee.Name,
                CertificateDate = c.CertificateDate,
                CertificatePlace = c.CertificatePlace
            }).ToListAsync();
        }

        // GET: api/Certificates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CertificatesDTO>> GetCertificatesDTO(int id)
        {
            var certificates = await _context.Certificates.Include(c => c.Employee).FirstOrDefaultAsync(c => c.ID == id);

            var certificatesDTO = new CertificatesDTO
            {
                ID = certificates.ID,
                Certificate = certificates.Certificate,
                EmployeeID = certificates.EmployeeID,
                EmployeeName = certificates.Employee.Name,
                CertificateDate = certificates.CertificateDate,
                CertificatePlace = certificates.CertificatePlace
            };
            if (certificatesDTO == null)
            {
                return NotFound();
            }

            return certificatesDTO;
        }

        [HttpGet]
        [Route("GetCertificatesByEmployeeID/{employeeID}")]
        public ActionResult<IEnumerable<CertificatesDTO>> GetCertificatesByEmployee(int employeeID)
        {
            var certificates =  _context.Certificates.Include(c => c.Employee)
                .Where(c=>c.EmployeeID==employeeID).Select(cer=> new CertificatesDTO
            {
                ID = cer.ID,
                Certificate = cer.Certificate,
                EmployeeID = cer.EmployeeID,
                EmployeeName = cer.Employee.Name,
                CertificateDate = cer.CertificateDate,
                CertificatePlace = cer.CertificatePlace
            }).ToList();
            if (certificates == null)
            {
                return NotFound();
            }

            return certificates;
        }
        // PUT: api/Certificates/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCertificatesDTO(int id, CertificatesDTO certificatesDTO)
        {
            if (id != certificatesDTO.ID)
            {
                return BadRequest();
            }
            Certificates certificates = new Certificates
            {
                ID = certificatesDTO.ID,
                Certificate = certificatesDTO.Certificate,
                EmployeeID = certificatesDTO.EmployeeID,
                CertificateDate = certificatesDTO.CertificateDate,
                CertificatePlace = certificatesDTO.CertificatePlace
            };

            _context.Entry(certificates).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificatesDTOExists(id))
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

        // POST: api/Certificates
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CertificatesDTO>> PostCertificatesDTO(CertificatesDTO certificatesDTO)
        {
            Certificates certificates = new Certificates
            {
                ID = certificatesDTO.ID,
                Certificate = certificatesDTO.Certificate,
                EmployeeID = certificatesDTO.EmployeeID,
                CertificateDate = certificatesDTO.CertificateDate,
                CertificatePlace = certificatesDTO.CertificatePlace
            };

            _context.Certificates.Add(certificates);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCertificatesDTO", new { id = certificatesDTO.ID }, certificatesDTO);
        }

        // DELETE: api/Certificates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Certificates>> DeleteCertificatesDTO(int id)
        {
            var certificatesDTO = await _context.Certificates.FindAsync(id);
            if (certificatesDTO == null)
            {
                return NotFound();
            }

            _context.Certificates.Remove(certificatesDTO);
            await _context.SaveChangesAsync();

            return certificatesDTO;
        }
        private bool CertificatesDTOExists(int id)
        {
            return _context.Certificates.Any(e => e.ID == id);
        }
    }
}
