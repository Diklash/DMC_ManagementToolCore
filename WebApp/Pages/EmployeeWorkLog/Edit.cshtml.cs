using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using WebApp.Models.ViewModels;

namespace WebApp.Pages.EmployeeWorkLog
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient; //inject the HttpClient using Ctor
        private readonly string _apiBaseUrl;

        [BindProperty]
        public DataAccessLibrary.Models.EmployeeWorkLog EmployeeWorkLogItem { get; set; }


        public EditModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiSettings:EmployeeWorkLog"];
        }

       
        public async Task<IActionResult> OnGetAsync(string EmployeeID)
        {
            //call to Get function
            var response = await _httpClient.GetAsync(_apiBaseUrl + "/SearchByEmployeeID?EmployeeID=" + EmployeeID);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                EmployeeWorkLogItem = JsonConvert.DeserializeObject<DataAccessLibrary.Models.EmployeeWorkLog>(content);

            }
            else
            {
                // Handle errors
            }

            return Page();
        }

        public async Task<IActionResult> OnPostEditAsync() //when the form submitted, the values of the form will come to this function, we can get all the values that has name
        {
            //2. property binding

            //var response = await _httpClient.GetAsync(_apiBaseUrl + "/SearchEmployeeWorkLog?EmployeeID=" + EmployeeWorkLogItem.EmployeeID);
            //if (response.IsSuccessStatusCode)
            //{
            //var content = await response.Content.ReadAsStringAsync();
            //Models.ViewModels.EmployeeWorkLog existingEmployeeWorkLog = JsonConvert.DeserializeObject<Models.ViewModels.EmployeeWorkLog>(content);

            //if (existingEmployeeWorkLog != null)
            //{
            //existingEmployeeWorkLog.SiteActivityID = EmployeeWorkLogItem.SiteActivityID;
            //existingEmployeeWorkLog.WorkingStartDate = EmployeeWorkLogItem.WorkingStartDate;
            //existingEmployeeWorkLog.WorkingEndDate = EmployeeWorkLogItem.WorkingEndDate;
            //existingEmployeeWorkLog.ProjectSiteID = EmployeeWorkLogItem.ProjectSiteID;

            //var json = JsonConvert.SerializeObject(existingEmployeeWorkLog);
            //var updatedContent = new StringContent(json, Encoding.UTF8, "application/json");

            var json = JsonConvert.SerializeObject(EmployeeWorkLogItem);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var updateResponse = await _httpClient.PutAsync(_apiBaseUrl, content /*updatedContent*/);
            if (updateResponse.IsSuccessStatusCode)// Handle success
            {
                //var responseContent = await updateResponse.Content.ReadAsStringAsync();
                //return RedirectToPage("/EmployeeWorkLog/List");

                //instead of redirect to another page , we will show success message on the screen.  ViewData is container for passing data between PageModel(.cs) and ViewModel(.cshtml) 
                //ViewData["MessageDescription"] = "Record was seccessfuly saved";

                //we want to have generic notifications
                var notifocation = new Notification
                {
                    Message = "Record was seccessfuly saved",
                    Type = Enums.NotificationType.Success
                };

                TempData["Notification"] = JsonConvert.SerializeObject(notifocation); //we need to serialize it becuse we pass it between diffrent pages (another page)
            }

            //}
            //}
            return RedirectToPage("/EmployeeWorkLog/List");
        }

        public async Task<IActionResult> OnPostDeleteAsync() //when the form submitted, the values of the form will come to this function, we can get all the values that has name
        {
            //2. property binding
            //TODO:move the logic to repository.....
            //var response = await _httpClient.GetAsync(_apiBaseUrl + "/SearchEmployeeWorkLog?EmployeeID=" + EmployeeWorkLogItem.EmployeeID);
            //if (response.IsSuccessStatusCode)
            //{
                //var content = await response.Content.ReadAsStringAsync();
                //Models.ViewModels.EmployeeWorkLog existingEmployeeWorkLog = JsonConvert.DeserializeObject<Models.ViewModels.EmployeeWorkLog>(content);

                //if (existingEmployeeWorkLog != null)
                //{
                    //var json = JsonConvert.SerializeObject(existingEmployeeWorkLog);
                    //var updatedContent = new StringContent(json, Encoding.UTF8, "application/json");

                    var updateResponse = await _httpClient.DeleteAsync(_apiBaseUrl + "/" + EmployeeWorkLogItem.EmployeeID);
                    if (updateResponse.IsSuccessStatusCode)// Handle success
                    {
                        var responseContent = await updateResponse.Content.ReadAsStringAsync();
                        return RedirectToPage("/EmployeeWorkLog/List");
                    }
                //}
            //}
            return Page();
        }
    }
}
