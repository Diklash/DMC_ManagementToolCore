using DataAccessLibrary.DataTransferObjects;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectSiteController : ControllerBase
    {
        private readonly IProjectSiteRepository projectSiteRepository;

        public ProjectSiteController(IProjectSiteRepository projectSiteRepository) //Dependency injection
        {
            this.projectSiteRepository = projectSiteRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectSiteDTO>> Get()
        {
            return await projectSiteRepository.Get();
        }

    }
}
