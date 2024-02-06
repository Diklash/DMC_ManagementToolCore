using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    [Table("SiteActivity")]
    public class SiteActivity
    {
        [Key]
        public int ID { get; set; }
        public string ActivityType { get; set; }
        
    }
}
