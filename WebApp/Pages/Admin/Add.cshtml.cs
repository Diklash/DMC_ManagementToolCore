using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace WebApp.Pages.Admin
{
    public class AddModel : PageModel
    {
        private readonly HttpClient _httpClient; //inject the HttpClient using Ctor
        private readonly string _apiBaseUrl;

        //[BindProperty] //this is attribute, instead of write each attribue here, we can create new class model with all the fields and create one propert of the class
        //public string Heading { get; set; }

        [BindProperty]
        public Models.ViewModels.AdminAdd AdminAdd { get; set; }

        public AddModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiSettings:WebApiAddress"] + "EmployeeWorkLog";
            //_apiBaseUrl = configuration["ApiSettings:EmployeeWorkLog"];
        }

        public void OnGet()
        {
            ////call to Get function
            //var response = _httpClient.GetAsync("https://localhost:7233/api/EmployeeWorkLog");
            //if (response.IsCompletedSuccessfully)
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //    var items = JsonConvert.DeserializeObject<List<Item>>(content);
            //    // Use items in your Razor Page
            //}
            //else
            //{
            //    // Handle errors
            //}

        }

        public async Task<IActionResult> OnPostAsync() //when the form submitted, the values of the form will come to this function, we can get all the values that has name
        {
            //1. read request object (not recomended)

            //var heading = Request.Form["heading"];
            //var pageTitle = Request.Form["pageTitle"];
            //var content = Request.Form["content"];
            //var shortDescription = Request.Form["shortDescription"];
            //var featuredImageURL = Request.Form["featuredImageURL"];
            //var publishedDate = Request.Form["publishedDate"];
            //var isVisiable = Request.Form["isVisiable"];

            //2. property binding
            int _projectSiteID;
            int.TryParse(AdminAdd.Content, out _projectSiteID);

            int _shortDescription;
            int.TryParse(AdminAdd.ShortDescription, out _shortDescription);

            var myData = new DataAccessLibrary.Models.EmployeeWorkLog
            {
                EmployeeID = AdminAdd.Heading,
                ProjectSiteID = _projectSiteID,
                SiteActivityID = _shortDescription,
                WorkingEndDate = AdminAdd.PublishedDate,
                WorkingStartDate = AdminAdd.PublishedDate
            };

            var json = JsonConvert.SerializeObject(myData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiBaseUrl, content);
            if (response.IsSuccessStatusCode)// Handle success
            {
                var responseContent = await response.Content.ReadAsStringAsync();
            }
            else  // Handle errors
            {

            }

            return Page();
        }
    }
}
