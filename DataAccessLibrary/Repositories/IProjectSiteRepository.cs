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
    }
}
