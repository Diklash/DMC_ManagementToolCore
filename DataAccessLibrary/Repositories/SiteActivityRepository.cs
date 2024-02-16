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
    public class SiteActivityRepository : ISiteActivityRepository
    {
        private readonly string connectionString;

        public SiteActivityRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task Create(SiteActivity item)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int siteId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SiteActivity>> Get()
        {
            IEnumerable<SiteActivity> siteActivityList = null;

            using (var conn = new SqlConnection(connectionString))
            {
                siteActivityList = await conn.QueryAsync<SiteActivity>("select * from SiteActivity");
            }

            return siteActivityList;
        }

        public async Task<SiteActivity> Get(SiteActivity item)
        {
            throw new NotImplementedException();
        }

        public async Task Update(SiteActivity item)
        {
            throw new NotImplementedException();
        }
    }
}
