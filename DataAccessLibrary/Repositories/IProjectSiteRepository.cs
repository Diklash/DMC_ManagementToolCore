using DataAccessLibrary.DataTransferObjects;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repositories
{
    public interface IProjectSiteRepository
    {
        Task<IEnumerable<ProjectSiteDTO>> Get();

        Task<ProjectSites> Get(ProjectSites item);

        Task<ProjectSiteDTO?> Get(int ID);

        Task Create(ProjectSites item);

        Task Update(ProjectSites item);

        Task<IEnumerable<ProjectStatus>> GetProjectStatusList();
    }
}
