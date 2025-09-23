using System.Collections;
using System.Collections.Generic;
using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(AppDbContext Context) : ControllerBase
    {
        [HttpGet("{UserId}")]
         public async Task <ActionResult<User>> GetUsers(string UserId)
        {
            var members =await  Context.Users.FindAsync(UserId);
            if (members == null) return NotFound("Not Found");
            return  members;

        }

    }
}
