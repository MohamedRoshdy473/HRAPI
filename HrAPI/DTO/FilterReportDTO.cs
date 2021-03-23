using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.DTO
{
    public class FilterReportDTO
    {
        public int ProfessionId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

    }
}
