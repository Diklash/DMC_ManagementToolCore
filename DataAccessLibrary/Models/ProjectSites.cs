using Dapper.Contrib.Extensions;

namespace DataAccessLibrary.Models
{
    [Table("ProjectSites")]
    public class ProjectSites
    {
        [Key]
        public int ID { get; set; }

        public string SiteName { get; set; }

        public string SiteAddress { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string SiteImageURL { get; set; }

        public string Remarks { get; set; }

        public int StatusID { get; set; }

        public decimal? FinancialAmount { get; set; }

        //[System.ComponentModel.DataAnnotations.Schema.NotMapped] // This annotation is optional and used for clarity.
        //public string StatusText { get; set; }
    }
}
