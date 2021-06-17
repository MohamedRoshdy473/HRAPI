using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.Models
{
    public class SchoolDepartments
    {
        public int Id { get; set; }
        public string SchoolDepartmentName { get; set; }
        public int SchoolId { get; set; }
        [ForeignKey("SchoolId")]
        public virtual School School { get; set; }
    }
}
