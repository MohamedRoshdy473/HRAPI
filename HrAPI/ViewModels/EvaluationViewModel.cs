using HrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.ViewModels
{
    public class EvaluationViewModel
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
       // public virtual Employee Employee { get; set; }
        public int EvaluationProfessionID { get; set; }
        public string EvaluationTypeName { get; set; }
        public int EvaluationTypeID { get; set; }
        public string ProfessionName { get; set; }
        public int ProfessionID { get; set; }
        // public virtual Profession Profession { get; set; }
        public decimal EvaluationDegreee { get; set; }
        public DateTime EvaluationDate { get; set; }
        public string Note { get; set; }
    }
}
