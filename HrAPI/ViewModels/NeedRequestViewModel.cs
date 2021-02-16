using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.ViewModels
{
    public class NeedRequestViewModel
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public DateTime NeedRequestDate { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
    }
}
