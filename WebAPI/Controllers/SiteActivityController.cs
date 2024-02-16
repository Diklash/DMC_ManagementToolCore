using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteActivityController : Controller
    {
        private readonly ISiteActivityRepository siteActivityRepository;

        public SiteActivityController(ISiteActivityRepository siteActivityRepository)
        {
            this.siteActivityRepository = siteActivityRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<SiteActivity>> Get()
        {
            return await siteActivityRepository.Get();
        }
    }
}
