using HrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.Services
{
    public interface IEmployeeRepository
    {
        Task<Employee> Get(int id);
        IEnumerable<Employee> GetAll();
        Task Add(Employee employee);
        void Delete(Employee employee);
        void Update(Employee employee);
    }
}
