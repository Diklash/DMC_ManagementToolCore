namespace WebApp.Models.ViewModels
{
    public class AdminAdd
    {
        public string Heading { get; set; }
        public string PageTitle { get; set; }

        public string Content { get; set; }

        public string ShortDescription { get; set; }

        public string FeaturedImageURL { get; set; }

        public DateTime PublishedDate { get; set; }

        public bool IsVisiable { get; set; }

    }
}
