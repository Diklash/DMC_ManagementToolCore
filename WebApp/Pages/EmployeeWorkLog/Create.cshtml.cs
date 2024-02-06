using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using WebApp.Models.ViewModels;

namespace WebApp.Pages.EmployeeWorkLog
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient; //inject the HttpClient using Ctor
        private readonly string _apiBaseUrl;

        //[BindProperty] //this is attribute, instead of write each attribue here, we can create new class model with all the fields and create one propert of the class
        //public string Heading { get; set; }

        [BindProperty]
        public DataAccessLibrary.Models.EmployeeWorkLog EmployeeWorkLog { get; set; }

        public CreateModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiSettings:EmployeeWorkLog"];
        }
        public void OnGet()
        {
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
            //var myData = new DataAccessLibrary.Models.EmployeeWorkLog
            //{
            //    EmployeeID = EmployeeWorkLog.EmployeeID,
            //    ProjectSiteID = EmployeeWorkLog.ProjectSiteID,
            //    SiteActivityID = EmployeeWorkLog.SiteActivityID,
            //    WorkingStartDate = EmployeeWorkLog.WorkingStartDate,
            //    WorkingEndDate = EmployeeWorkLog.WorkingEndDate
            //};

            var json = JsonConvert.SerializeObject(EmployeeWorkLog/*myData*/);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiBaseUrl, content);
            if (response.IsSuccessStatusCode)// Handle success
            {
                //var responseContent = await response.Content.ReadAsStringAsync();

                //instead of redirect to another page , we will show success message on the screen.  ViewData is container for passing data between PageModel(.cs) and ViewModel(.cshtml) 
                //TempData["MessageDescription"] = "Record was created.";

                //we want to have generic notifications
                var notifocation = new Notification
                {
                    Message = "Record was created.",
                    Type = Enums.NotificationType.Success
                };

                TempData["Notification"] = JsonConvert.SerializeObject(notifocation); //we need to serialize it becuse we pass it between diffrent pages (another page)

                return RedirectToPage("/EmployeeWorkLog/List");
            }
            else  // Handle errors
            {
                //we want to have generic notifications
                var notifocation = new Notification
                {
                    Message = "Record wasn't created.",
                    Type = Enums.NotificationType.Error
                };

                TempData["Notification"] = JsonConvert.SerializeObject(notifocation);
            }

            return Page();
        }
    }
}
