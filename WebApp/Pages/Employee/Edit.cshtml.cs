using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using WebApp.Models.ViewModels;

namespace WebApp.Pages.Employee
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient; //inject the HttpClient using Ctor
        private readonly string _apiBaseUrl;

        [BindProperty]
        public DataAccessLibrary.Models.Employee Employee { get; set; }


        [BindProperty]
        public IFormFile UploadImage { get; set; }

        [BindProperty]
        public string ImageUrlAPI { get; set; }

        public EditModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiSettings:Employee"];
            ImageUrlAPI = configuration["ApiSettings:Images"];
        }

        public async Task<IActionResult> OnGetAsync(string EmployeeID)
        {
            //call to Get function
            var response = await _httpClient.GetAsync(_apiBaseUrl + "/" + EmployeeID);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Employee = JsonConvert.DeserializeObject<DataAccessLibrary.Models.Employee>(content);
            }
            else
            {
                // Handle errors
            }

            return Page();
        }

        public async Task<IActionResult> OnPostEditAsync() //when the form submitted, the values of the form will come to this function, we can get all the values that has name
        {
            var json = JsonConvert.SerializeObject(Employee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var updateResponse = await _httpClient.PutAsync(_apiBaseUrl, content);
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
            return RedirectToPage("/Employee/List");
        }
    }
}
