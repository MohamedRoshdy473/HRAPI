using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.DTO
{
    public class FacultyDepartmentDTO
    {
        public int Id { get; set; }
        public string FacultyDepartmentName { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public int UniversityID { get; set; }
        public string UniversityName { get; set; }

    }
}
