using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.DTO
{
    public class ProfessionDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ManagerID { get; set; }
        public string ManagerName { get; set; }
    }
}
