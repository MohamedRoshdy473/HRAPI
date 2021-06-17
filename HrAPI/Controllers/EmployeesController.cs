using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HrAPI.Models;
using HrAPI.DTO;
using HrAPI.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace HrAPI.Controllers
{
    //[Authorize(AuthenticationSchemes =
    //JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        //private readonly EmployeeCore employeeCore;
        // private readonly IHostingEnvironment hostingEnvironment;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        //private readonly string email;
        //private readonly Employee empCurrentUser;

        public EmployeesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
            //System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            //email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            //empCurrentUser = _context.Employees.Where(e => e.Email == email).FirstOrDefault();
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);


        // GET: api/Employees
        [HttpGet]
        public IEnumerable<EmployeeDTO> GetEmployees()
        {

            var emps = _context.Employees.Where(e => e.IsActive == true).Select(e => new EmployeeDTO
            {
                ID = e.ID,
                Name = e.Name,
                ProfessionID = e.ProfessionID,
                ProfessionName = e.Profession.Name,
                GraduatioYear = e.GraduatioYear,
                Address = e.Address,
                Code = e.Code,
                DateOfBirth = e.DateOfBirth,
                Email = e.Email,
                gender = e.gender,
                HiringDateHiringDate = e.HiringDateHiringDate,
                MaritalStatus = e.MaritalStatus,
                Phone = e.Phone,
                RelevantPhone = e.RelevantPhone,
                Photo = e.photo,
                EmailCompany = e.EmailCompany,
                Mobile = e.Mobile,
                NationalId = e.NationalId,
                FacultyDepartmentName = e.FacultyDepartment.FacultyDepartmentName,
                FacultyDepartmentId = (int)e.FacultyDepartmentId,
                FacultyName = e.FacultyDepartment.Faculty.FacultyName,
                UniversityName = e.FacultyDepartment.Faculty.University.UniversityName,
                PositionId = e.PositionId,
                PositionName = e.Positions.PositionName,
                PositionlevelId = e.PositionlevelId,
                LevelName = e.PositionLevel.LevelName,
                IsActive = e.IsActive,
                SchoolDepartmentId = e.SchoolDepartmentsId,
                SchoolDepartmentName = e.SchoolDepartments.SchoolDepartmentName,

            }).ToList();
            return emps;
        }
        [Route("GetUnworkedEmployees")]
        public IEnumerable<EmployeeDTO> GetUnworkedEmployees()
        {

            var emps = _context.Employees.Where(e => e.IsActive == false).Select(e => new EmployeeDTO
            {
                ID = e.ID,
                Name = e.Name,
                ProfessionID = e.ProfessionID,
                ProfessionName = e.Profession.Name,
                GraduatioYear = e.GraduatioYear,
                Address = e.Address,
                Code = e.Code,
                DateOfBirth = e.DateOfBirth,
                Email = e.Email,
                gender = e.gender,
                HiringDateHiringDate = e.HiringDateHiringDate,
                MaritalStatus = e.MaritalStatus,
                Phone = e.Phone,
                RelevantPhone = e.RelevantPhone,
                Photo = e.photo,
                EmailCompany = e.EmailCompany,
                Mobile = e.Mobile,
                NationalId = e.NationalId,
                FacultyDepartmentName = e.FacultyDepartment.FacultyDepartmentName,
                FacultyDepartmentId = (int)e.FacultyDepartmentId,
                FacultyName = e.FacultyDepartment.Faculty.FacultyName,
                UniversityName = e.FacultyDepartment.Faculty.University.UniversityName,
                PositionId = e.PositionId,
                PositionName = e.Positions.PositionName,
                PositionlevelId = e.PositionlevelId,
                LevelName = e.PositionLevel.LevelName,
                IsActive = e.IsActive,
                SchoolDepartmentId = e.SchoolDepartmentsId,
                SchoolDepartmentName = e.SchoolDepartments.SchoolDepartmentName,

            }).ToList();
            return emps;
        }
        // GET: api/Employees/5
        [HttpGet("{id}")]
        public EmployeeDTO GetEmployee(int id)
        {
           //  e = new Employee();
            EmployeeDTO emp=new EmployeeDTO();
               Employee e = _context.Employees.Where(e => e.ID == id).Include(e => e.Profession).Include(e => e.FacultyDepartment)
                .Include(e => e.FacultyDepartment.Faculty).Include(e => e.FacultyDepartment.Faculty.University)
                .Include(e => e.PositionLevel).Include(e => e.Positions).Include(e => e.SchoolDepartments)
                .Include(e => e.SchoolDepartments.School).FirstOrDefault();
            try
            {
                //school
                if (e.FacultyDepartment == null && e.SchoolDepartments != null)
                {
                    emp = new EmployeeDTO
                    {
                        ID = e.ID,
                        Name = e.Name,
                        ProfessionName = e.Profession.Name,
                        ProfessionID = e.Profession.ID,
                        GraduatioYear = e.GraduatioYear,
                        Address = e.Address,
                        Code = e.Code,
                        DateOfBirth = e.DateOfBirth,
                        Email = e.Email,
                        gender = e.gender,
                        HiringDateHiringDate = e.HiringDateHiringDate,
                        MaritalStatus = e.MaritalStatus,
                        Phone = e.Phone,
                        RelevantPhone = e.RelevantPhone,
                        Photo = e.photo,
                        EmailCompany = e.EmailCompany,
                        Mobile = e.Mobile,
                        NationalId = e.NationalId,
                        PositionId = e.PositionId,
                        PositionName = e.Positions.PositionName,
                        PositionlevelId = e.PositionlevelId,
                        LevelName = e.PositionLevel.LevelName,
                        Education = e.Education,
                        SchoolDepartmentId = e.SchoolDepartmentsId,
                        SchoolDepartmentName = e.SchoolDepartments.SchoolDepartmentName,
                        SchoolId = e.SchoolDepartments.SchoolId,
                        SchoolName = e.SchoolDepartments.School.SchoolName
                    };
                }
              //no education
                else if (e.SchoolDepartments == null && e.FacultyDepartment == null)
                {
                    emp = new EmployeeDTO
                    {
                        ID = e.ID,
                        Name = e.Name,
                        ProfessionName = e.Profession.Name,
                        ProfessionID = e.Profession.ID,
                        GraduatioYear = e.GraduatioYear,
                        Address = e.Address,
                        Code = e.Code,
                        DateOfBirth = e.DateOfBirth,
                        Email = e.Email,
                        gender = e.gender,
                        HiringDateHiringDate = e.HiringDateHiringDate,
                        MaritalStatus = e.MaritalStatus,
                        Phone = e.Phone,
                        RelevantPhone = e.RelevantPhone,
                        Photo = e.photo,
                        EmailCompany = e.EmailCompany,
                        Mobile = e.Mobile,
                        NationalId = e.NationalId,
                        PositionId = e.PositionId,
                        PositionName = e.Positions.PositionName,
                        PositionlevelId = e.PositionlevelId,
                        LevelName = e.PositionLevel.LevelName,
                        Education = e.Education,
                    };
                }
               //faculty
                else
                {
                    emp = new EmployeeDTO
                    {
                        ID = e.ID,
                        Name = e.Name,
                        ProfessionName = e.Profession.Name,
                        ProfessionID = e.Profession.ID,
                        GraduatioYear = e.GraduatioYear,
                        Address = e.Address,
                        Code = e.Code,
                        DateOfBirth = e.DateOfBirth,
                        Email = e.Email,
                        gender = e.gender,
                        HiringDateHiringDate = e.HiringDateHiringDate,
                        MaritalStatus = e.MaritalStatus,
                        Phone = e.Phone,
                        RelevantPhone = e.RelevantPhone,
                        Photo = e.photo,
                        EmailCompany = e.EmailCompany,
                        Mobile = e.Mobile,
                        NationalId = e.NationalId,
                        PositionId = e.PositionId,
                        PositionName = e.Positions.PositionName,
                        PositionlevelId = e.PositionlevelId,
                        LevelName = e.PositionLevel.LevelName,
                        Education = e.Education,

                        FacultyDepartmentName = e.FacultyDepartment.FacultyDepartmentName,
                        FacultyDepartmentId = e.FacultyDepartmentId,
                        FacultyId = e.FacultyDepartment.FacultyId,
                        FacultyName = e.FacultyDepartment.Faculty.FacultyName,
                        UniversityId = e.FacultyDepartment.Faculty.UniversityID,
                        UniversityName = e.FacultyDepartment.Faculty.University.UniversityName,
                    };

                }
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
            if (emp == null)
            {
                return null;
            }

            return emp;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutEmployee(int id, EmployeeDTO e)
        {
            if (id != e.ID)
            {
                return BadRequest();
            }
            if (e.FacultyDepartmentId==0)
            {
                e.FacultyDepartmentId = null;
            }
            if (e.SchoolDepartmentId==0)
            {
                e.SchoolDepartmentId = null;
            }
            Employee employee = new Employee();
            employee.ID = e.ID;
            employee.Name = e.Name;
            employee.Code = e.Code;
            employee.ProfessionID = e.ProfessionID;
            employee.gender = e.gender;
            employee.Address = e.Address;
            employee.DateOfBirth = e.DateOfBirth;
            employee.MaritalStatus = e.MaritalStatus;
            employee.GraduatioYear = e.GraduatioYear;
            employee.Phone = e.Phone;
            employee.RelevantPhone = e.RelevantPhone;
            employee.Email = e.Email;
            employee.photo = e.Photo;
            employee.HiringDateHiringDate = e.HiringDateHiringDate;
            employee.Mobile = e.Mobile;
            employee.EmailCompany = e.EmailCompany;
            employee.NationalId = e.NationalId;
            employee.Education = e.Education;
            employee.IsActive = true;
            employee.PositionId = e.PositionId;
            employee.PositionlevelId = e.PositionlevelId;
            employee.FacultyDepartmentId = e.FacultyDepartmentId;
            employee.SchoolDepartmentsId = e.SchoolDepartmentId;
            _context.Entry(employee).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public int PostEmployee(EmployeeDTO e)
        {
            if (e.FacultyDepartmentId == 0)
            {
                e.FacultyDepartmentId = null;
            }
            if (e.SchoolDepartmentId == 0)
            {
                e.SchoolDepartmentId = null;
            }
            Employee employee = new Employee();
            employee.Name = e.Name;
            employee.Code = e.Code;
            employee.ProfessionID = e.ProfessionID;
            employee.gender = e.gender;
            employee.Address = e.Address;
            employee.DateOfBirth = e.DateOfBirth;
            employee.MaritalStatus = e.MaritalStatus;
            employee.GraduatioYear = e.GraduatioYear;
            employee.Phone = e.Phone;
            employee.RelevantPhone = e.RelevantPhone;
            employee.Email = e.Email;
            employee.photo = e.Photo;
            employee.HiringDateHiringDate = e.HiringDateHiringDate;
            employee.Mobile = e.Mobile;
            employee.EmailCompany = e.EmailCompany;
            employee.NationalId = e.NationalId;
            employee.Education = e.Education;
            employee.IsActive = true;
            employee.PositionId = e.PositionId;
            employee.PositionlevelId = e.PositionlevelId;
            employee.FacultyDepartmentId = e.FacultyDepartmentId;
            employee.SchoolDepartmentsId = e.SchoolDepartmentId;

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return employee.ID;
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.ID == id);
        }
        [HttpPost]
        [Route("UploadImage")]
        public ActionResult UploadFile(IFormFile file)
        {
            var ImagesTypes = new List<string>() { "image/jpg", "image/jpeg", "image/png" };
            var FileTypes = new List<string>() { "application/pdf", "application/doc", "application/docs" };
            //var user = GetCurrentUserAsync();
            //var emp = _context.Employees.Where(e => e.Email == user.Result.Email).FirstOrDefault();
            string path;
            if (ImagesTypes.Contains(file.ContentType))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", file.FileName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

            }
            else //if(FileTypes.Contains(file.ContentType))
            {
                //path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", file.FileName);
                //using (Stream stream = new FileStream(path, FileMode.Create))
                //{
                //    file.CopyTo(stream);
                //}
                return BadRequest();
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getImage/{ImageName}")]
        public IActionResult ImageGet(string ImageName)
        {
            //ImageName = "#6M@CX79)G77LT&9F&G8^P0XYA2^YNE9J2GO^WCA.jpg";
            if (ImageName == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot/images", ImageName);

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

        [AllowAnonymous]
        [HttpGet]
        [Route("getFile/{FName}")]
        public IActionResult getFile(string FName)
        {
            //FName = "H4QV1OHX0A7H5ZQ1EEE4I004TMKRBF79XTZONS1J.jpg";
            if (FName == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot/leaves/", FName);

            var memory = new MemoryStream();
            var ext = System.IO.Path.GetExtension(path);
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            var contentType = "APPLICATION/pdf";
            //return File(Path.GetFileName(path), contentType, FName);
            return File(memory, contentType, Path.GetFileName(path));
        }

        //for current user
        [HttpGet]
        [Route("EmployeeByProfession")]
        public IEnumerable<EmployeeDTO> EmployeeByProfession()
        {
            List<EmployeeDTO> lstEmployeeDTO = new List<EmployeeDTO>();
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var LstEmpUsers = _context.Employees.Where(e => e.Email == email).ToList();
            if (LstEmpUsers.Count > 0)
            {
                var EmployeeObj = LstEmpUsers[0];


                lstEmployeeDTO = _context.Employees.Where(e => e.ProfessionID == EmployeeObj.ProfessionID && e.Email != email).Select(e => new EmployeeDTO
                {
                    ID = e.ID,
                    Name = e.Name,
                    ProfessionName = e.Profession.Name,
                    GraduatioYear = e.GraduatioYear,
                    Address = e.Address,
                    Code = e.Code,
                    DateOfBirth = e.DateOfBirth,
                    Email = e.Email,
                    gender = e.gender,
                    HiringDateHiringDate = e.HiringDateHiringDate,
                    MaritalStatus = e.MaritalStatus,
                    Phone = e.Phone,
                    RelevantPhone = e.RelevantPhone,
                    Photo = e.photo,

                    EmailCompany = e.EmailCompany,
                    Mobile = e.Mobile,
                    NationalId = e.NationalId,
                    FacultyDepartmentName = e.FacultyDepartment.FacultyDepartmentName,
                    FacultyDepartmentId = (int)e.FacultyDepartmentId,
                    FacultyName = e.FacultyDepartment.Faculty.FacultyName,
                    UniversityName = e.FacultyDepartment.Faculty.University.UniversityName,
                    PositionId = e.PositionId,
                    PositionName = e.Positions.PositionName,
                    PositionlevelId = e.PositionlevelId,
                    LevelName = e.PositionLevel.LevelName

                }).ToList();
            }
            return lstEmployeeDTO;
        }
        [HttpGet]
        [Route("GetAllEmployeesByProfession/{id}")]
        public IEnumerable<EmployeeDTO> GetAllEmployeesByProfession(int id)
        {
            var emps = _context.Employees.Where(e => e.ProfessionID == id).Select(e => new EmployeeDTO
            {
                ID = e.ID,
                Name = e.Name,
                ProfessionName = e.Profession.Name,
                GraduatioYear = e.GraduatioYear,
                Address = e.Address,
                Code = e.Code,
                DateOfBirth = e.DateOfBirth,
                Email = e.Email,
                gender = e.gender,
                HiringDateHiringDate = e.HiringDateHiringDate,
                MaritalStatus = e.MaritalStatus,
                Phone = e.Phone,
                RelevantPhone = e.RelevantPhone,
                Photo = e.photo
            }).ToList();
            return emps;
        }

    }
}
