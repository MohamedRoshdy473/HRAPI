using HrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.DTO
{
    public class LeaveReportDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int ProfessionId { get; set; }
        public string ProfessionName { get; set; }
        public List<LeaveRequest> lstLeaveRequest { get; set; }
    }
}
