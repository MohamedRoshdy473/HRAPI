using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.DTO
{
    public class AttendanceDTO
    {
        public int ID { get; set; }
        public int AttID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Profession { get; set; }
        public DateTime time { get; set; }
        public string DepTime { get; set; }
        public string photo { get; set; }
    }
}
