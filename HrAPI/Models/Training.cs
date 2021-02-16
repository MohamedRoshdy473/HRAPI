using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.Models
{
    public class Training
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Certified { get; set; }
        public string TrainingPlace { get; set; }
        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }
        public int InstructorID { get; set; }
        [ForeignKey("InstructorID")]
        public virtual Instructor Instructor { get; set; }
        public int TrainingProfessionID { get; set; }
        [ForeignKey("TrainingProfessionID")]
        public virtual TrainingProfessions TrainingProfessions { get; set; }

    }
}
