using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.Models
{
    public class NeedsRequest
    {
        //public enum NeedsTYPE
        //{
        //    stationary,
        //    computerDevices
        //};
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }
        // public string Type { get; set; }
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual NeedsCategory needsCategory { get; set; }
        public int SubCategoryID { get; set; }

        [ForeignKey("SubCategoryID")]
        public virtual SubCategory subCategory { get; set; }
        public DateTime NeedRequestDate { get; set; }
        public string Status { get; set; }

        public string Comment { get; set; }

    }
}
