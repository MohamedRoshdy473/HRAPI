using HrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.DTO
{
    public class AttendanceGroupedDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public List<Attendance> lstAttendance { get; set; }
    }
}
