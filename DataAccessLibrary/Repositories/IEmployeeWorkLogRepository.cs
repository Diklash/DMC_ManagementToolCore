using DataAccessLibrary.DataTransferObjects;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repositories
{
    public interface IEmployeeWorkLogRepository
    {
        Task<IEnumerable<EmployeeWorkLogDTO>> Get();
        Task<EmployeeWorkLog> Get(EmployeeWorkLog item);
        Task<EmployeeWorkLog> GetWorkLogByEmployeeAndSite(EmployeeWorkLog item);
        Task<EmployeeWorkLog> Get(string EmployeeID);

        Task Create(EmployeeWorkLog employee);
        Task Delete(int employeeId);
        Task Update(EmployeeWorkLog employee);
    }
}
