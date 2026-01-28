using Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Member(IMemberRepo memberRepo) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            var Data= await memberRepo.GetMembersAsync();
            if(Data.Any())
            return Ok(Data);
        return NotFound();
             
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Getmember(string id)
        {
            var member=await memberRepo.GetMemberByIdAsync(id);
            if(member==null)
            return NotFound();
            return Ok(member);
        }
        [HttpGet("{id}/photo")]
        public async Task<ActionResult> GetmemberPhoto(string id)
        {
            var member=await memberRepo.GetMemberPhotosAsync(id);
            if(member==null)
            return NotFound();
            return Ok(member);
        }
    }
}
