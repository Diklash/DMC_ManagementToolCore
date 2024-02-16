using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using WebApp.Models.ViewModels;

namespace WebApp.Pages.ProjectSite
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient; //inject the HttpClient using Ctor
        private readonly string _apiBaseUrl;


        [BindProperty]
        public DataAccessLibrary.DataTransferObjects.ProjectSiteDTO ProjectSite { get; set; }

        [BindProperty]
        public List<DataAccessLibrary.DataTransferObjects.ProjectSiteDTO> ProjectSitesList { get; set; }

        public EditModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiSettings:WebApiAddress"] + "ProjectSite";
        }


        public async Task<IActionResult> OnGetAsync(string ProjectSiteID)
        {
            //call to Get function
            var response = await _httpClient.GetAsync(_apiBaseUrl + "/" + ProjectSiteID);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ProjectSite = JsonConvert.DeserializeObject<DataAccessLibrary.DataTransferObjects.ProjectSiteDTO>(content);

                GetProjectSiteList();
            }
            else
            {
                // Handle errors
            }

            return Page();
        }

        public async Task<IActionResult> OnPostEditAsync() //when the form submitted, the values of the form will come to this function, we can get all the values that has name
        {
            var json = JsonConvert.SerializeObject(ProjectSite.ProjectSite);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var updateResponse = await _httpClient.PutAsync(_apiBaseUrl, content);
            if (updateResponse.IsSuccessStatusCode)// Handle success
            {
                //we want to have generic notifications
                var notifocation = new Notification
                {
                    Message = "Record was seccessfuly saved",
                    Type = Enums.NotificationType.Success
                };

                TempData["Notification"] = JsonConvert.SerializeObject(notifocation); //we need to serialize it becuse we pass it between diffrent pages (another page)
            }

            return RedirectToPage("/ProjectSite/List");
        }

        private async Task GetProjectSiteList()
        {
            var responseProjectSite = await _httpClient.GetAsync(_apiBaseUrl);
            if (responseProjectSite.IsSuccessStatusCode)
            {
                var contentProjectSite = await responseProjectSite.Content.ReadAsStringAsync();
                ProjectSitesList = JsonConvert.DeserializeObject<List<DataAccessLibrary.DataTransferObjects.ProjectSiteDTO>>(contentProjectSite);

                DataAccessLibrary.DataTransferObjects.ProjectSiteDTO projectSiteItem = ProjectSitesList.Find(x => x.ProjectSite.ID == ProjectSite.ProjectSite.ID);

                ProjectSite = projectSiteItem;
            }
        }
    }
}
