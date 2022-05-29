using CovidStatsRequestService.Configuration;
using CovidStatsRequestService.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CovidStatsRequestService.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        
        private readonly IHttpClientFactory _clientFactory;
        private readonly JWTAuthenticationManager _jWTAuthenticationManager;
        private  APIConfigurationOptions _apiConfigurationOptions;

        public RequestController( IHttpClientFactory clientFactory, 
            JWTAuthenticationManager jWTAuthenticationManager,
            IOptions<APIConfigurationOptions> apiConfigurationOptions)
        {
            
            _clientFactory = clientFactory;
            _jWTAuthenticationManager = jWTAuthenticationManager;
            _apiConfigurationOptions = apiConfigurationOptions.Value;
        }
        
        [Authorize]
        [HttpGet("Covid19Summary")]
        public async Task<ActionResult> Covid19Summary()
        {

            try
            {
                HttpResponseMessage response = await ExternalCovidAPI(_apiConfigurationOptions.Summary);

                if (response.IsSuccessStatusCode)
                {
                    return Ok(response.Content.ReadAsStringAsync());
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex) 
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           

        }


        [Authorize]
        [HttpGet("Covid19UAEHistory")]
        public async Task<ActionResult> Covid19UAEHistory()
        {
            try
            {
                HttpResponseMessage response = await ExternalCovidAPI(_apiConfigurationOptions.UAECovidHisotry);

                if (response.IsSuccessStatusCode)
                {
                    return Ok(response.Content.ReadAsStringAsync());
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest);
            }

            

        }

        [AllowAnonymous]
        [HttpPost("Authorize")]
        public IActionResult AuthUser([FromBody] Models.User user)
        {
            try
            {
                var token = _jWTAuthenticationManager.Authenticate(user.usrename, user.password);

                if (token == null)
                {
                    return Unauthorized();
                }
                return Ok(token);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest);
            }
            
        }
        async Task<HttpResponseMessage> ExternalCovidAPI(string endpoint)
        {
            var client = _clientFactory.CreateClient("AssignmentClient");

            var response = await client.GetAsync(_apiConfigurationOptions.BaseURL + endpoint);
            return response;
        }
    }
}
