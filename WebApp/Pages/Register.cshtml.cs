using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using WebApp.Models.ViewModels;

namespace WebApp.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly HttpClient _httpClient; //inject the HttpClient using Ctor
        private readonly string _apiBaseUrl;
      
        [BindProperty]
        public Models.ViewModels.Register RegisterViewModel { get; set; }

        public RegisterModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiSettings:User"];
        }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var user = new IdentityUser
            {
                UserName = RegisterViewModel.UserName,
                Email = RegisterViewModel.Email
            };

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiBaseUrl + "/Create", content);
           
            var errorNotification = new Notification
            {
                Message = "Something went wrong.",
                Type = Enums.NotificationType.Error
            };

            ViewData["Notification"] = errorNotification;

            return Page();
        }
    }
}
