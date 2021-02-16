using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.Models
{
    public class SubCategory
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual NeedsCategory needsCategory { get; set; }
    }
}
