using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.Models
{
    public class Evaluation
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }
        public int EvaluationProfessionID { get; set; }
        [ForeignKey("EvaluationProfessionID")]
        public virtual EvaluationProfession evaluationProfession { get; set; }
        public decimal EvaluationDegreee { get; set; }
        public DateTime EvaluationDate { get; set; }
        public string Note { get; set; }

    }
}
