using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.DTO
{
    public class SchoolDepartmentsDTO
    {
        public int Id { get; set; }
        public string SchoolDepartmentName { get; set; }
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
    }
}
