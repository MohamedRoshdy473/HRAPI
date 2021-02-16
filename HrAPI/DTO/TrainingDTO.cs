using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.DTO
{
    public class TrainingDTO
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Certified { get; set; }
        public string TrainingPlace { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName  { get; set; }
        public int InstructorID { get; set; }
        public string InstructorName { get; set; }
        public int TrainingProfessionID { get; set; }
        public int ProfessionID { get; set; }
        public string ProfessionName { get; set; }
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int TrainingTypeID { get; set; }
        public string TrainingTypeName { get; set; }
    }
}
