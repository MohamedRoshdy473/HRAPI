using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.DTO
{
    public class EmployeeDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ProfessionName { get; set; }
        public int ProfessionID { get; set; }
        public string gender { get; set; }
        public string Address { get; set; }
        public string DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public string GraduatioYear { get; set; }
        public string Phone { get; set; }
        public string RelevantPhone { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string HiringDateHiringDate { get; set; }

        public string Mobile { get; set; }
        public string EmailCompany { get; set; }
        public string NationalId { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public int PositionlevelId { get; set; }
        public string LevelName  { get; set; }
        public int? FacultyDepartmentId { get; set; }
        public string FacultyDepartmentName { get; set; }
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public int? SchoolDepartmentId { get; set; }
        public string SchoolDepartmentName { get; set; }
        public int? SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string Education { get; set; }
        public Boolean IsActive { get; set; }

    }
}
