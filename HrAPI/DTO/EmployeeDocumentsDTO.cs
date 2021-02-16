using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.DTO
{
    public class EmployeeDocumentsDTO
    {
        public int Id { get; set; }
        public string DocumentName { get; set; }
        public string FileName { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
    }
}
