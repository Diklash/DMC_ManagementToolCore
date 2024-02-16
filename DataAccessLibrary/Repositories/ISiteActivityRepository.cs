using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repositories
{
    public interface ISiteActivityRepository
    {
        Task<IEnumerable<SiteActivity>> Get();

        Task<SiteActivity> Get(SiteActivity item);

        Task Create(SiteActivity item);
        Task Delete(int siteId);
        Task Update(SiteActivity item);
    }
}
