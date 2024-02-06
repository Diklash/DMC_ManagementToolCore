using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    [Table("Employee")]
    public class ProjectStatus
    {
        [Key]
        public int ID { get; set; }

        public string Text { get; set; }
    }
}
