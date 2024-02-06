using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly HttpClient _httpClient; //inject the HttpClient using Ctor
        private readonly string _apiBaseUrl;

        [BindProperty]
        public List<DataAccessLibrary.DataTransferObjects.ProjectSiteDTO> ProjectSitesList { get; set; }

        public IndexModel(ILogger<IndexModel> logger, HttpClient httpClient, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiSettings:ProjectSite"];
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //call to Get function
            var response = await _httpClient.GetAsync(_apiBaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ProjectSitesList = JsonConvert.DeserializeObject<List<DataAccessLibrary.DataTransferObjects.ProjectSiteDTO>>(content);
            }
            else
            {
                // Handle errors
            }

            return Page();
        }
    }
}