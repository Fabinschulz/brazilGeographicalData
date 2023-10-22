using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BrazilGeographicalData.src.Services.Controllers
{
    [Route("authenticated")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpGet]
        [Authorize]
        public IActionResult Get(ClaimsPrincipal user)
        {
            return Ok(new
            {
                message = $"Authenticated user: {user.Identity.Name}"
            });
        }

    }
}
