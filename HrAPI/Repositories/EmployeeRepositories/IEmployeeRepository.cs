using HrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetAllEMployee();
    }
}
