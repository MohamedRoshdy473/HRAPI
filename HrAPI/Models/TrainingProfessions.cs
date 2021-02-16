using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.Models
{
    public class TrainingProfessions
    {
        public int ID { get; set; }
        public int ProfessionID { get; set; }
        [ForeignKey("ProfessionID")]
        public virtual Profession Profession { get; set; }
        public int CourseID { get; set; }
        [ForeignKey("CourseID")]
        public virtual Courses Courses { get; set; }
    }
}
