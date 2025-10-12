using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugTest : ControllerBase
    {
        [HttpGet("auth")]
        public IActionResult NotAuth()
        {
            return Unauthorized();

        }
        [HttpGet("serverError")]
        public IActionResult serverError()
        {
            throw new Exception("error");
        }
    }
}
