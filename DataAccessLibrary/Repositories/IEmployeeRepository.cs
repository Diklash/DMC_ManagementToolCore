using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> Get();

        Task<Employee> Get(int id);
        Task Create(Employee employee);
        Task Delete(Employee employeeId);
        Task Update(Employee employee);

    }
}
