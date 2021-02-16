using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.Models
{
    public class Courses
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string CourseName { get; set; }
        public int TrainingTypeID { get; set; }
        [ForeignKey("TrainingTypeID")]
        public virtual TrainingType TrainingType{get;set;}
    }
}
