 using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.Models
{
    public class Employee 
    {
        public enum Gender
        {
            Male,
            Female
        };
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int ProfessionID { get; set; }
        [ForeignKey("ProfessionID")]
        public virtual Profession Profession { get; set; }        
        public string gender { get; set; }
        public string Address { get; set; }
        public string DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public string GraduatioYear { get; set; }
        public string Phone { get; set; }
        public string RelevantPhone { get; set; }
        public string Email { get; set; }
        public string photo { get; set; }
        public  string HiringDateHiringDate { get; set; }
        public int AllowedLeaveDays { get; set; }


        public string Mobile { get; set; }
        public string EmailCompany { get; set; }
        public string NationalId { get; set; }
        public string Education { get; set; }
        public Boolean IsActive  { get; set; }
        public int PositionId { get; set; }
        [ForeignKey("PositionId")]
        public virtual Positions Positions { get; set; }
        public int PositionlevelId { get; set; }
        [ForeignKey("PositionlevelId")]
        public virtual PositionLevel PositionLevel { get; set; }
        public int? FacultyDepartmentId { get; set; }
        [ForeignKey("FacultyDepartmentId")]
        public virtual FacultyDepartment FacultyDepartment { get; set; }
        public int? SchoolDepartmentsId { get; set; }
        [ForeignKey("SchoolDepartmentsId")]
        public virtual SchoolDepartments SchoolDepartments { get; set; }
    }
}
