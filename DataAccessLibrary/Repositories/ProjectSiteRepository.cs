using Dapper;
using DataAccessLibrary.DataTransferObjects;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repositories
{
    public class ProjectSiteRepository : IProjectSiteRepository
    {
        private readonly string connectionString;

        //ctor +tab
        public ProjectSiteRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<ProjectSiteDTO>> Get()
        {
            IEnumerable<ProjectSiteDTO> projectSites = null;

            using (var conn = new SqlConnection(connectionString))
            {
                //conn.QueryAsync<ProjectSites, string, ProjectSiteDTO>  (ProjectSites = first object type, string = second object type (StatusText), ProjectSiteDTO The return object type)
                projectSites = await conn.QueryAsync<ProjectSites, string, ProjectSiteDTO>(@"SELECT ps.*, StatusText = pstatus.Text 
                                                                            FROM ProjectSites ps 
                                                                            LEFT JOIN ProjectStatus pstatus ON ps.StatusID = pstatus.ID",
                                                                            (projectSite, statusText) =>
                                                                            {
                                                                                return new ProjectSiteDTO
                                                                                {
                                                                                    ProjectSite = projectSite,
                                                                                    StatusText = statusText
                                                                                };
                                                                            },
                                                                            splitOn: "StatusText"
                                                                    );
            }

            return projectSites;
        }
    }
}
