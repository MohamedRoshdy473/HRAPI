using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HrAPI.DTO;
using HrAPI.Models;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace HrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDocumentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeDocumentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeDocumentses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDocumentsDTO>>> GetEmployeeDocumentsDTO()
        {
            return await _context.EmployeeDocumentsDTO.ToListAsync();
        }

        // GET: api/EmployeeDocumentses/5
        [HttpGet("{id}")]
        public ActionResult<EmployeeDocumentsDTO> GetEmployeeDocumentsDTO(int id)
        {
            var employeeDocument = _context.EmployeeDocuments.Include(d => d.Employee).FirstOrDefault(d => d.Id == id);
            var employeeDocumentsDTO = new EmployeeDocumentsDTO()
            {
                Id = employeeDocument.Id,
              DocumentName=employeeDocument.DocumentName,
              EmployeeID=employeeDocument.EmployeeID,
              FileName=employeeDocument.FileName,
              EmployeeName=employeeDocument.Employee.Name
            };
            return employeeDocumentsDTO;

            
        }

        [HttpGet]
        [Route("GetEmployeeDocumentsByEmployeeId/{employeeId}")]
        public IEnumerable<EmployeeDocumentsDTO> GetEmployeeDocumentsByEmployeeId(int employeeId)
        {
            var employeeDocument = _context.EmployeeDocuments.Where(d => d.EmployeeID == employeeId).Select(employeeDocument =>
               new EmployeeDocumentsDTO()
               {
                   Id = employeeDocument.Id,
                   EmployeeID = employeeDocument.EmployeeID,
                   EmployeeName=employeeDocument.Employee.Name,
                   DocumentName=employeeDocument.DocumentName,
                   FileName=employeeDocument.FileName
               }).ToList();
            return employeeDocument;
           // return _projectDocumentRepository.GetProjectDocumentsByProjectId(ProjectId);
        }

        // PUT: api/EmployeeDocumentses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeDocumentsDTO(int id, EmployeeDocumentsDTO employeeDocumentsDTO)
        {
            if (id != employeeDocumentsDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(employeeDocumentsDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeDocumentsDTOExists(id))
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

        // POST: api/EmployeeDocumentses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public void PostEmployeeDocumentsDTO(List<EmployeeDocumentsDTO> employeeDocumentsDTO)
        {
            foreach (var item in employeeDocumentsDTO)
            {
                EmployeeDocuments employeeDocuments = new EmployeeDocuments();
                employeeDocuments.Id = item.Id;
                employeeDocuments.DocumentName = item.DocumentName;
                employeeDocuments.FileName = item.FileName;
                employeeDocuments.EmployeeID = item.EmployeeID;
                _context.Add(employeeDocuments);
            }
            _context.SaveChanges();

           // return CreatedAtAction("GetEmployeeDocumentsDTO", new { id = employeeDocumentsDTO.Id }, employeeDocumentsDTO);
        }

        // DELETE: api/EmployeeDocumentses/5
        [HttpDelete("{id}")]
        public void DeleteEmployeeDocumentsDTO(int id)
        {
            EmployeeDocuments employeeDocument = _context.EmployeeDocuments.Find(id);
            _context.EmployeeDocuments.Remove(employeeDocument);
            _context.SaveChanges();
        }

        private bool EmployeeDocumentsDTOExists(int id)
        {
            return _context.EmployeeDocumentsDTO.Any(e => e.Id == id);
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("uploadfile")]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("wwwroot","documentFiles");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"the error is {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getImage/{docName}")]
        public IActionResult ImageGet(string ImageName)
        {
            //ImageName = "#6M@CX79)G77LT&9F&G8^P0XYA2^YNE9J2GO^WCA.jpg";
            if (ImageName == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot/documentFiles", ImageName);

            var memory = new MemoryStream();
            var ext = System.IO.Path.GetExtension(path);
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            var contentType = "APPLICATION/octet-stream";
            return File(memory, contentType, Path.GetFileName(path));
        }



    }
}
