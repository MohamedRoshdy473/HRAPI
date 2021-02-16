using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.Models
{
    public class FacultyDepartment
    {
        public int Id { get; set; }
        public string FacultyDepartmentName { get; set; }
        public int FacultyId { get; set; }
        [ForeignKey("FacultyId")]
        public virtual Faculty Faculty { get; set; }
    }
}
