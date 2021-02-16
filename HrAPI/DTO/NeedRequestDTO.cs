using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.DTO
{
    public class NeedRequestDTO
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public DateTime NeedRequestDate { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
    }
}
