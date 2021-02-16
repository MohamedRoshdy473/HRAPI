using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.DTO
{
    public class CoursesDTO
    {
        public int ID { get; set; }
        public string CourseName { get; set; }
        public int TrainingTypeID { get; set; }
        public string TrainingTypeName { get; set; }

    }
}
