using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.DTO
{
    public class CertificatesDTO
    {
        public int ID { get; set; }
        public string Certificate { get; set; }
        public DateTime CertificateDate { get; set; }
        public string CertificatePlace { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }

    }
}
