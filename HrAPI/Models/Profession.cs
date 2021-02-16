using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.Models
{
    public class Profession
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int ManagerID { get; set; }
        [ForeignKey("ManagerID")]
        public Employee Manager { get; set; }
        [InverseProperty("Profession")]
        public virtual List<Employee> Employees { get; set; }
    }
}
