using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.DTO
{
    public class TrainingProfessionsDTO
    {
        public int ID { get; set; }
        public int ProfessionID { get; set; }
        public string ProfessionName { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int TrainingTypeID { get; set; }
        public string TrainingTypeName { get; set; }
    }
}
