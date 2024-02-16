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


        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<ProjectSiteDTO?> Get(int id)
        {
            return await projectSiteRepository.Get(id);
        }

        [HttpPost]
        public async Task Post([FromBody] ProjectSites item)
        {
            if (!string.IsNullOrEmpty(item.SiteName) && item.ID > 0)
            {
                ProjectSites currentItem = await projectSiteRepository.Get(item);
                if (currentItem == null)
                {
                    await projectSiteRepository.Create(item);
                }
            }

        }

        // PUT api/<EmployeeWorkLogController>/5
        [HttpPut]//HttpPut("{id}")
        public async Task Put([FromBody] ProjectSites item)
        {
            await projectSiteRepository.Update(item);
        }


        [HttpGet("GetProjectStatusList")]
        public async Task<IEnumerable<ProjectStatus>> GetProjectStatusList()
        {
            return await projectSiteRepository.GetProjectStatusList();
        }

        

    }
}
