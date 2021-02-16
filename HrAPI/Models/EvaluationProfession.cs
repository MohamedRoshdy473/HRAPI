using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.Models
{
    public class EvaluationProfession
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int ProfessionID { get; set; }
        [ForeignKey("ProfessionID")]
        public virtual Profession profession { get; set; }
        public int EvaluationTypeID { get; set; }
        [ForeignKey("EvaluationTypeID")]
        public virtual EvaluationType evaluationType { get; set; }
    }
}
