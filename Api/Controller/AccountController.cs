using System.Security.Cryptography;
using System.Text;
using Api.Data;
using Api.DTOs;
using Api.Entities;
using Api.Extensions;
using Api.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(AppDbContext context,ITokenService tokenService) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var hmac = new HMACSHA512();
            var user = new User()
            {
                Name = registerDTO.Name,
                Email = registerDTO.Email,
                PassWordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PassWordSalt = hmac.Key

            };
            if (await EmailExist(registerDTO.Email))
            {
                return BadRequest("Email Exist");
            }
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return UserExtention.ToDTO(user, tokenService);
        }
        private async Task<bool> EmailExist(string email)
        {
            return await context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == loginDTO.Email);
            if (user == null) return Unauthorized("Invalid User");
            var hmac = new HMACSHA512(user.PassWordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            if (!computedHash.SequenceEqual(user.PassWordHash))
                return Ok("password Invalid");

            else
                return user.ToDTO(tokenService);
            
        }


    }
}
