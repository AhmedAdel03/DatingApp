using System.Collections;
using System.Collections.Generic;
using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(AppDbContext Context) : ControllerBase
    {
        [HttpGet()]
         public async Task <ActionResult<List<User>>> GetUsers()
        {
            var members =await  Context.Users.ToListAsync();
            if (members == null) return NotFound("Not Found");
            return members;

        }

    }
}
