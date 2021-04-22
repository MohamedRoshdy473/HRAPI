using HrAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.Repositories.EmployeeRepositories
{
    public class EmployeeRepositories : Repository<Employee>, IRepository<Employee>
    {
        private readonly DbContext _Context;
        public EmployeeRepositories(DbContext context) : base(context)
        {

        }

    }
}
