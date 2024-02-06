using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    [Table("EmployeeWorkLog")]
    public class EmployeeWorkLog
    {
        [Key]
        public int ID { get; set; }

        public string EmployeeID { get; set; }

        public int? ProjectSiteID { get; set; }

        public DateTime? WorkingStartDate { get; set; }

        public DateTime? WorkingEndDate { get; set; }

        public int? SiteActivityID { get; set; }

        public string Remarks { get; set; }
    }
}
