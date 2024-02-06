using Dapper;
using Dapper.Contrib.Extensions;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repositories
{
    /*
     Repository is a design pattern acts like middel layer between the application and DataAccessLogic/DataAccessLibrary
     */
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string connectionString;

        //ctor +tab
        public EmployeeRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<Employee>> Get()
        {
            IEnumerable<Employee> employeeList = null;
            using (var conn = new SqlConnection(connectionString))
            {
                employeeList = await conn.QueryAsync<Employee>("select * from Employee");
            }

            return employeeList;
        }
        public async Task<Employee> Get(int id)
        {
            Employee employee;

            using (var conn = new SqlConnection(connectionString))
            {
                employee = await conn.QueryFirstOrDefaultAsync<Employee>("select * from Employee where EmployeeID = " + id);
            }

            return employee;

        }
        public async Task Create(Employee employee)
        {
            long? newRateTaxId;

            using (var conn = new SqlConnection(connectionString))
            {
                newRateTaxId = await conn.InsertAsync<Employee>(employee);
            }
        }
        public async Task Delete(Employee employee)
        {
            bool wasUpdated;


            using (var conn = new SqlConnection(connectionString))
            {
                wasUpdated = await conn.DeleteAsync<Employee>(employee);
            }

            //return wasUpdated;
        }
        public async Task Update(Employee employee)
        {
            bool wasUpdated;

            using (var conn = new SqlConnection(connectionString))
            {
                wasUpdated = await conn.UpdateAsync<Employee>(employee);
            }

            //return wasUpdated;
        }
    }
}
