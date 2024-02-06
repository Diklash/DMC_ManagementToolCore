using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataTransferObjects
{
    public class EmployeeWorkLogDTO
    {
        public EmployeeWorkLog EmployeeWorkLog { get; set; }

        // Include the additional property from Employee
        public string EmployeeName { get; set; }

        // Include the additional property from ProjectSite
        public string ProjectSiteName { get; set; }

        public string SiteActivityName { get; set; }

        
    }
}
