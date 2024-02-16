using DataAccessLibrary.DataTransferObjects;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using WebApp.Models.ViewModels;

namespace WebApp.Pages.ProjectSite
{
    public class ListModel : PageModel
    {
        private readonly HttpClient _httpClient; //inject the HttpClient using Ctor
        private readonly string _apiBaseUrl;

        [BindProperty]
        public List<ProjectSiteDTO> ProjectSitesList { get; set; }

        [BindProperty]
        public ProjectSiteDTO ProjectSite { get; set; }

        public ListModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiSettings:WebApiAddress"] + "ProjectSite";
        }

        public async Task<IActionResult> OnGetAsync()
        {
            
            //we used generic notification

            var notificationJson = TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonConvert.DeserializeObject<Models.ViewModels.Notification>(notificationJson.ToString());
            }

            //call to Get function
            var response = await _httpClient.GetAsync(_apiBaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ProjectSitesList = JsonConvert.DeserializeObject<List<ProjectSiteDTO>>(content);
            }
            else
            {
                // Handle errors
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var json = JsonConvert.SerializeObject(ProjectSite);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiBaseUrl, content);
            if (response.IsSuccessStatusCode)// Handle success
            {
                //we want to have generic notifications
                var notifocation = new Notification
                {
                    Message = "Record was created.",
                    Type = Enums.NotificationType.Success
                };

                TempData["Notification"] = JsonConvert.SerializeObject(notifocation); //we need to serialize it becuse we pass it between diffrent pages (another page)

                return Page();
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
