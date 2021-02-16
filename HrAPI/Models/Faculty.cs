using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public string FacultyName { get; set; }
        public int UniversityID { get; set; }
        [ForeignKey("UniversityID")]
        public virtual University University { get; set; }
    }
}
