using COVID19_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace COVID19_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Covid19StatsController : ControllerBase
    {

        private readonly JWTAuthenticationManager jWTAuthenticationManager;
       


        public Covid19StatsController(JWTAuthenticationManager jWTAuthenticationManager)
        {
            this.jWTAuthenticationManager = jWTAuthenticationManager;
        }

        [Authorize]
        [HttpGet("Home")]
        public  string Get()
        {
            return "Covid 19 Status API v.1";
        }


        [AllowAnonymous]
        [HttpPost("Authorize")]
        public IActionResult AuthUser([FromBody] User user)
        {
            var token = jWTAuthenticationManager.Authenticate(user.usrename, user.password);

            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }




    }

    
}