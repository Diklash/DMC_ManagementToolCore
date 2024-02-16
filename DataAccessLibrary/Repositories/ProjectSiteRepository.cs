using Dapper;
using Dapper.Contrib.Extensions;
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

        public async Task<ProjectSiteDTO> Get(int ID)
        {
            ProjectSiteDTO? projectSites = null;

            using (var conn = new SqlConnection(connectionString))
            {
                projectSites = (await conn.QueryAsync<ProjectSites, string, ProjectSiteDTO>(@"SELECT ps.*, StatusText = pstatus.Text 
                                                                            FROM ProjectSites ps 
                                                                            LEFT JOIN ProjectStatus pstatus ON ps.StatusID = pstatus.ID
                                                                            WHERE ps.ID = @ProjectSiteID",
                                                                            (projectSite, statusText) =>
                                                                            {
                                                                                return new ProjectSiteDTO
                                                                                {
                                                                                    ProjectSite = projectSite,
                                                                                    StatusText = statusText
                                                                                };
                                                                            },
                                                                            new { ProjectSiteID = ID },
                                                                            splitOn: "StatusText"
                                                                    )).FirstOrDefault();
            }

            return projectSites;
        }

        public async Task<ProjectSites> Get(ProjectSites item)
        {
            ProjectSites projectSite;

            using (var conn = new SqlConnection(connectionString))
            {
                projectSite = await conn.QueryFirstOrDefaultAsync<ProjectSites>("select * from ProjectSites where ID = '" + item.ID + "'");
            }

            return projectSite;
        }

        public async Task Create(ProjectSites item)
        {
            long? newRateTaxId;

            using (var conn = new SqlConnection(connectionString))
            {
                newRateTaxId = await conn.InsertAsync<ProjectSites>(item);
            }
        }

        public async Task Update(ProjectSites item)
        {
            bool wasUpdated;

            ProjectSites existingProjectSites = await Get(item);

            if (existingProjectSites != null)
            {
                existingProjectSites.Remarks = item.Remarks;
                existingProjectSites.StartDate = item.StartDate;
                existingProjectSites.EndDate = item.EndDate;
                existingProjectSites.SiteImageURL = item.SiteImageURL;
                existingProjectSites.FinancialAmount = item.FinancialAmount;
                existingProjectSites.SiteName = item.SiteName;
                existingProjectSites.StatusID = item.StatusID;
                existingProjectSites.SiteAddress = item.SiteAddress;

                using (var conn = new SqlConnection(connectionString))
                {
                    wasUpdated = await conn.UpdateAsync<ProjectSites>(existingProjectSites);
                }
            }
        }

        public async Task<IEnumerable<ProjectStatus>> GetProjectStatusList()
        {
            IEnumerable<ProjectStatus> projectStatus;

            using (var conn = new SqlConnection(connectionString))
            {
                projectStatus = await conn.QueryAsync<ProjectStatus>("select * from ProjectStatus");
            }

            return projectStatus;
 
        }

    }
}
