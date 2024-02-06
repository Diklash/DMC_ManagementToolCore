using DataAccessLibrary.DataTransferObjects;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeWorkLogController : ControllerBase
    {
        private readonly IEmployeeWorkLogRepository employeeWorkLogRepository;

        public EmployeeWorkLogController(IEmployeeWorkLogRepository employeeWorkLogRepository) //Dependency injection
        {
            this.employeeWorkLogRepository = employeeWorkLogRepository;
        }


        // GET: api/<EmployeeWorkLogController>
        [HttpGet]
        public async Task<IEnumerable<EmployeeWorkLogDTO>> Get()
        {
            return await employeeWorkLogRepository.Get();
        }

        // GET api/<EmployeeWorkLogController>/5
        [HttpGet("SearchByEmployeeID")]
        public async Task<EmployeeWorkLog> SearchByEmployeeID(string EmployeeID)
        {
            return await employeeWorkLogRepository.Get(EmployeeID);
        }


        //GET api/<EmployeeWorkLogController>/5
        [HttpGet("SearchEmployeeWorkLog")]
        public async Task<EmployeeWorkLog> SearchEmployeeWorkLog([FromBody] EmployeeWorkLog item)
        {
            return await employeeWorkLogRepository.Get(item);
        }

        // POST api/<EmployeeWorkLogController>
        [HttpPost]
        public async Task Post([FromBody] EmployeeWorkLog item)
        {
            if (!string.IsNullOrEmpty(item.EmployeeID) && item.ProjectSiteID.HasValue)
            {
                EmployeeWorkLog currentItem = await employeeWorkLogRepository.GetWorkLogByEmployeeAndSite(item);
                if (currentItem == null)
                {
                    await employeeWorkLogRepository.Create(item);
                }
            }

        }

        // PUT api/<EmployeeWorkLogController>/5
        [HttpPut]//HttpPut("{id}")
        public async Task Put([FromBody] EmployeeWorkLog item)
        {
            await employeeWorkLogRepository.Update(item);
        }

        // DELETE api/<EmployeeWorkLogController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await employeeWorkLogRepository.Delete(id);
        }
    }
}
