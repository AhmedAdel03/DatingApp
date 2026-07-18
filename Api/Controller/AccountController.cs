using System.Security.Cryptography;
using System.Text;
using Api.Data;
using Api.DTOs;
using Api.Entities;
using Api.Extensions;
using Api.Interface;
using Api.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAccountService accountService,ITokenService tokenService) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var result = await accountService.RegisterAsync(registerDTO);
            if (result == null) return Ok("Email Already exist");
            else
            {
                return  result;
            } 

        }
        
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            try
            {
               var result= await accountService.LoginAsync(loginDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                
                return Unauthorized(ex.Message);
            }
            
            
        }
        [HttpPost("RefreshToken")]
        public async Task<ActionResult<UserDTO>> RefreshToken(RefreshTokenDTo refreshTokenDTo)
        {
             var user=await tokenService.CheckUserAndRefreshToken(refreshTokenDTo.RefreshToken);
             if (user==null)
            {
                return Unauthorized();
            }
            return await UserExtention.ToDTO(user,tokenService);
            
        }


    }
}
