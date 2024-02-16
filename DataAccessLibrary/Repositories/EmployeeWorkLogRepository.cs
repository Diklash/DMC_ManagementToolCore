using Dapper;
using Dapper.Contrib.Extensions;
using DataAccessLibrary.DataTransferObjects;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repositories
{
    public class EmployeeWorkLogRepository : IEmployeeWorkLogRepository
    {
        private readonly string connectionString;

        //ctor +tab
        public EmployeeWorkLogRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //TODO: need to change all the repository methods to work async
        public async Task<IEnumerable<EmployeeWorkLogDTO>> Get()
        {
            //IEnumerable<EmployeeWorkLogDTO> employeeList = null;
            //using (var conn = new SqlConnection(connectionString))
            //{
            //    employeeList = await conn.QueryAsync<EmployeeWorkLogDTO>("select * from EmployeeWorkLog WITH (NOLOCK) where IsActive = 1");
            //}

            //return employeeList;

            IEnumerable<EmployeeWorkLogDTO> employeeList = null;

            using (var conn = new SqlConnection(connectionString))
            {
                //conn.QueryAsync<ProjectSites, string, ProjectSiteDTO>  (ProjectSites = first object type, string = second object type (StatusText), ProjectSiteDTO The return object type)
                employeeList = await conn.QueryAsync<EmployeeWorkLog, string,string, string, EmployeeWorkLogDTO>(@"SELECT wrklg.*, EmployeeName = emp.FirstName + ' ' + emp.LastName , ProjectSiteName = prjs.SiteName, SiteActivityName = sta.ActivityType
                                                                            FROM EmployeeWorkLog wrklg 
                                                                            LEFT JOIN Employee emp ON wrklg.EmployeeID = emp.EmployeeID
                                                                            LEFT JOIN ProjectSites prjs ON wrklg.ProjectSiteID = prjs.ID
                                                                            LEFT JOIN SiteActivity sta ON wrklg.SiteActivityID = sta.ID
                                                                            where wrklg.IsActive = 1",
                                                                            (employeeWorkLog, employeeName, projectSiteName, siteActivityName) =>
                                                                            {
                                                                                return new EmployeeWorkLogDTO
                                                                                {
                                                                                    EmployeeWorkLog = employeeWorkLog,
                                                                                    EmployeeName = employeeName,
                                                                                    ProjectSiteName = projectSiteName,
                                                                                    SiteActivityName = siteActivityName
                                                                                };
                                                                            },
                                                                            splitOn: "EmployeeName,ProjectSiteName,SiteActivityName"
                                                                    );
            }

            return employeeList;
        }

        public async Task<EmployeeWorkLog> Get(string EmployeeID)
        {

            using (var conn = new SqlConnection(connectionString))
            {
                var result = await conn.QueryAsync<EmployeeWorkLog>("SELECT * FROM EmployeeWorkLog WHERE EmployeeID = @EmployeeID", new { EmployeeID });
                return result.FirstOrDefault();
            }

            //EmployeeWorkLog employee = null;
            //using (var conn = new SqlConnection(connectionString))
            //{
            //    employee = await conn.QueryAsync<EmployeeWorkLog>("select * from EmployeeWorkLog where EmployeeID = '" + EmployeeID + "'").FirstOrDefault();
            //}

            //return employee;
        }



        //public async Task<IEnumerable<EmployeeWorkLog>> GetAsync()
        //{
        //    IEnumerable<EmployeeWorkLog> employeeList = null;
        //    using (var conn = new SqlConnection(connectionString))
        //    {
        //        await conn.OpenAsync(); // Open the connection asynchronously
        //        employeeList = await conn.QueryAsync<EmployeeWorkLog>("select * from EmployeeWorkLog WITH (NOLOCK) where IsActive = 1");
        //    }

        //    return employeeList;
        //}

        public async Task<EmployeeWorkLog> Get(EmployeeWorkLog item)
        {
            EmployeeWorkLog employee;

            using (var conn = new SqlConnection(connectionString))
            {
                employee = await conn.QueryFirstOrDefaultAsync<EmployeeWorkLog>("select * from EmployeeWorkLog where EmployeeID = '" + item.EmployeeID + "'");
            }

            return employee;

        }

        public async Task<EmployeeWorkLog> Get(int employeeID)
        {
            EmployeeWorkLog employee;

            using (var conn = new SqlConnection(connectionString))
            {
                employee = await conn.QueryFirstOrDefaultAsync<EmployeeWorkLog>("select * from EmployeeWorkLog where EmployeeID = " + employeeID);
            }

            return employee;

        }

        public async Task<EmployeeWorkLog> GetWorkLogByEmployeeAndSite(EmployeeWorkLog item)
        {
            //var parameters = new { EmployeeID = item.EmployeeID, ProjectSiteID = item.ProjectSiteID };
            //using (var conn = new SqlConnection(connectionString))
            //{
            //    employee = await conn.QueryMultipleAsync(@"DMC.dbo.GetWorkLogByEmployeeAndSite", parameters, commandType: CommandType.StoredProcedure).Read<EmployeeWorkLog>().FirstOrDefault();
            //}
            //return employee;

            var parameters = new { EmployeeID = item.EmployeeID, ProjectSiteID = item.ProjectSiteID };

            using (var conn = new SqlConnection(connectionString))
            {
                using (var gridReader = await conn.QueryMultipleAsync(@"DMC.dbo.GetWorkLogByEmployeeAndSite", parameters, commandType: CommandType.StoredProcedure))
                {
                    return gridReader.Read<EmployeeWorkLog>().FirstOrDefault();
                }
            }

        }

        public async Task Create(EmployeeWorkLog employee)
        {
            long? newRateTaxId;

            using (var conn = new SqlConnection(connectionString))
            {
                newRateTaxId = await conn.InsertAsync<EmployeeWorkLog>(employee);
            }
        }
        public async Task Delete(int employeeId)
        {
            bool wasUpdated;

            EmployeeWorkLog existingEmployeeWorkLog = await Get(employeeId);

            if (existingEmployeeWorkLog != null)
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    wasUpdated = await conn.DeleteAsync<EmployeeWorkLog>(existingEmployeeWorkLog);
                }
            }

            //return wasUpdated;
        }


        public async Task Update(EmployeeWorkLog employee)
        {
            bool wasUpdated;

            EmployeeWorkLog existingEmployeeWorkLog = await Get(employee);

            if (existingEmployeeWorkLog != null)
            {
                existingEmployeeWorkLog.SiteActivityID = employee.SiteActivityID;
                existingEmployeeWorkLog.WorkingStartDate = employee.WorkingStartDate;
                existingEmployeeWorkLog.WorkingEndDate = employee.WorkingEndDate;
                existingEmployeeWorkLog.ProjectSiteID = employee.ProjectSiteID;
                existingEmployeeWorkLog.Remarks = employee.Remarks;

                using (var conn = new SqlConnection(connectionString))
                {
                    wasUpdated = await conn.UpdateAsync<EmployeeWorkLog>(existingEmployeeWorkLog);
                }
            }

            //return wasUpdated;
        }
    }
}
