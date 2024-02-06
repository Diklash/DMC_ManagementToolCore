//using DataAccessLibrary.Repositories;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//namespace WebAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly IAuthenticationRepository authenticationRepository;

//        public UserController(IAuthenticationRepository authenticationRepository)
//        {
//            this.authenticationRepository = authenticationRepository;
//        }

//        [HttpPost]
//        public async Task Create([FromBody] IdentityUser item)
//        {
//            CancellationToken cancellationToken = new CancellationToken(false);
//            await authenticationRepository.CreateAsync(item, cancellationToken);
//        }
//    }
//}
