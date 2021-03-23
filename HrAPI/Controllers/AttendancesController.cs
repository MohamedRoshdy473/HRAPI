using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HrAPI.Models;
using Microsoft.Data.SqlClient.DataClassification;
using HrAPI.DTO;

namespace HrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Attendances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendance()
        {
            return await _context.Attendance.ToListAsync();
        }
        [HttpGet]
        [Route("getTodayAttendedIN")]
        public async Task<ActionResult<IEnumerable<AttendanceDTO>>> getTodayAttendedIN()
        {
            //var attenList= _context.Attendance.Where(a=>a.Arrival.Date==DateTime.Now.Date).ToListAsync();
            var lstEmployeeAttendance = (from attend in _context.Attendance
                                         join emp in _context.Employees on attend.EmployeeID equals emp.ID
                                         where attend.Arrival.Date == DateTime.Today.Date
                                         select new AttendanceDTO
                                         {
                                             time = attend.Arrival,
                                             Code = emp.Code,
                                             AttID = attend.ID,
                                             ID = emp.ID,
                                             Name = emp.Name,
                                             photo = emp.photo,
                                             Profession = emp.Profession.Name
                                         }).ToList();


            return lstEmployeeAttendance;
        }
        [HttpGet]
        [Route("getTodayAttendedOUT")]
        public async Task<ActionResult<IEnumerable<AttendanceDTO>>> getTodayAttendedOUT()
        {
            //var attenList= _context.Attendance.Where(a=>a.Arrival.Date==DateTime.Now.Date).ToListAsync();
            var lstEmployeeAttendance = (from attend in _context.Attendance
                                         join emp in _context.Employees on attend.EmployeeID equals emp.ID
                                         where attend.Departure.Date == DateTime.Today.Date
                                         select new AttendanceDTO
                                         {
                                             time = attend.Departure,
                                             Code = emp.Code,
                                             AttID = attend.ID,
                                             ID = emp.ID,
                                             Name = emp.Name,
                                             photo = emp.photo,
                                             Profession = emp.Profession.Name
                                         }).ToList();
            return lstEmployeeAttendance;
        }
        [Route("GetAttendances")]
        public IEnumerable<AttendanceGroupedDTO> GetAttendances()
        {
            List<AttendanceGroupedDTO> list = new List<AttendanceGroupedDTO>();
            var lstEmployees = _context.Employees.ToList();
            foreach (var emp in lstEmployees)
            {
                AttendanceGroupedDTO attendanceGrouped = new AttendanceGroupedDTO();
                attendanceGrouped.EmployeeId = emp.ID;
                attendanceGrouped.EmployeeName = emp.Name;
                attendanceGrouped.lstAttendance = _context.Attendance.Where(att => att.EmployeeID == emp.ID).ToList();
                list.Add(attendanceGrouped);
            }
            return list;
        }
        [Route("GetAttendancesByProfessionId/{ProfessionId}")]
        public IEnumerable<AttendanceGroupedDTO> GetAttendancesByProfessionId(int ProfessionId)
        {
            List<AttendanceGroupedDTO> list = new List<AttendanceGroupedDTO>();
            var lstEmployees = _context.Employees.Where(e=>e.ProfessionID==ProfessionId).ToList();
            foreach (var emp in lstEmployees)
            {
                AttendanceGroupedDTO attendanceGrouped = new AttendanceGroupedDTO();
                attendanceGrouped.EmployeeId = emp.ID;
                attendanceGrouped.EmployeeName = emp.Name;
                attendanceGrouped.lstAttendance = _context.Attendance.Where(att => att.EmployeeID == emp.ID).ToList();
                list.Add(attendanceGrouped);
            }
            return list;
        }
        [Route("GetAttendancesByProfessionIdAndEmployeeId/{ProfessionId}/{EmployeeId}")]
        public IEnumerable<AttendanceGroupedDTO> GetAttendancesByProfessionIdAndEmployeeId(int ProfessionId,int EmployeeId)
        {
            List<AttendanceGroupedDTO> list = new List<AttendanceGroupedDTO>();
            var lstEmployees = _context.Employees.Where(e => e.ProfessionID == ProfessionId && e.ID==EmployeeId).ToList();
            foreach (var emp in lstEmployees)
            {
                AttendanceGroupedDTO attendanceGrouped = new AttendanceGroupedDTO();
                attendanceGrouped.EmployeeId = emp.ID;
                attendanceGrouped.EmployeeName = emp.Name;
                attendanceGrouped.lstAttendance = _context.Attendance.Where(att => att.EmployeeID == emp.ID && att.Employee.ProfessionID == ProfessionId).ToList();
                list.Add(attendanceGrouped);
            }
            return list;
        }
        [Route("GetAttendancesByProfessionIdAndEmployeeIdAndDate/{ProfessionId}/{EmployeeId}/{startDate}/{endDate}")]
        public IEnumerable<AttendanceGroupedDTO> GetAttendancesByProfessionIdAndEmployeeIdAndDate(int ProfessionId, int EmployeeId, DateTime startDate, DateTime endDate)
        {
            List<AttendanceGroupedDTO> list = new List<AttendanceGroupedDTO>();
            var lstEmployees = _context.Employees.Where(e => e.ProfessionID == ProfessionId && e.ID == EmployeeId).ToList();
            foreach (var emp in lstEmployees)
            {
                AttendanceGroupedDTO attendanceGrouped = new AttendanceGroupedDTO();
                attendanceGrouped.EmployeeId = emp.ID;
                attendanceGrouped.EmployeeName = emp.Name;
                attendanceGrouped.lstAttendance = _context.Attendance.Where(att => att.EmployeeID == emp.ID && att.Employee.ProfessionID == ProfessionId
                 && att.Arrival >= startDate && att.Departure <= endDate).ToList();
                list.Add(attendanceGrouped);
            }
            return list;
        }
        [Route("GetAttendancesByDate/{startDate}/{endDate}")]
        public IEnumerable<AttendanceGroupedDTO> GetAttendancesByDate(DateTime startDate, DateTime endDate)
        {
            List<AttendanceGroupedDTO> list = new List<AttendanceGroupedDTO>();
            var lstEmployees = _context.Employees.ToList();
            foreach (var emp in lstEmployees)
            {
                AttendanceGroupedDTO attendanceGrouped = new AttendanceGroupedDTO();
                attendanceGrouped.EmployeeId = emp.ID;
                attendanceGrouped.EmployeeName = emp.Name;
                attendanceGrouped.lstAttendance = _context.Attendance.Where(att => att.Arrival >= startDate && att.Departure <= endDate && att.EmployeeID == emp.ID).ToList();
                list.Add(attendanceGrouped);
            }
            return list;
        }
        // GET: api/Attendances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> GetAttendance(int id)
        {
            var attendance = await _context.Attendance.FindAsync(id);

            if (attendance == null)
            {
                return NotFound();
            }

            return attendance;
        }

        // PUT: api/Attendances/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendance(int id, Attendance attendance)
        {
            attendance.Departure = attendance.Departure.ToLocalTime();
            if (id != attendance.ID)
            {
                return BadRequest();
            }
            var oldAttend = _context.Attendance.Find(id);
            oldAttend.Departure = attendance.Departure;
            _context.Entry(oldAttend).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(id))
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

        // POST: api/Attendances
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Attendance>> PostAttendance(Attendance attendance)
        {
            attendance.Arrival = attendance.Arrival.ToLocalTime();
            int x = 0;
            int y = x;
            _context.Attendance.Add(attendance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttendance", new { id = attendance.ID }, attendance);
        }

        // DELETE: api/Attendances/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Attendance>> DeleteAttendance(int id)
        {
            var attendance = await _context.Attendance.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            _context.Attendance.Remove(attendance);
            await _context.SaveChangesAsync();

            return attendance;
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendance.Any(e => e.ID == id);
        }
    }
}
