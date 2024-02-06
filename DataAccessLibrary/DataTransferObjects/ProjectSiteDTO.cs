using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataTransferObjects
{
    public class ProjectSiteDTO
    {
        public ProjectSites ProjectSite { get; set; }

        // Include the additional property from ProjectStatus
        public string StatusText { get; set; }
    }
}
