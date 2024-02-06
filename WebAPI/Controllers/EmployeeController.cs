using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.AspNetCore.Mvc;

//https://www.youtube.com/watch?v=f31p-TU4uV4&list=PLXCqSX1D2fd-hTje-LDCWx6lRrfGTZRGB&index=20
//see this video for postman calls to api 


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;
      
        public EmployeeController(IEmployeeRepository employeeRepository/*, IEmployeeProcessor employeeProcessor*/) //Dependency injection
        {
            this.employeeRepository = employeeRepository;
            //this.employeeProcessor = employeeProcessor;
        }
      
        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await employeeRepository.Get();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<Employee> Get(int id)
        {
            return await employeeRepository.Get(id);
        }

    
        [HttpPost]
        public async Task Post([FromBody] Employee employee)
        {
            await employeeRepository.Create(employee);
        }

       
        [HttpPut]
        public async Task Put([FromBody] Employee employee)
        {
            await employeeRepository.Update(employee);
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            Employee employee = await employeeRepository.Get(id);
            if (employee != null)
            {
                await employeeRepository.Delete(employee);
            }
        }
    }

}
