using System.Security.Claims;
using Api.Data.Repositories;
using Api.DTOs;
using Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Member(IMemberRepo memberRepo, IRepository<Member> repository) : ControllerBase
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
        
            [HttpPatch]
  public async Task<ActionResult> UpdateMember(UpdateProfileDTO updateProfileDTO)
        {
            var memberId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(memberId==null) return NotFound();
            var member= await memberRepo.GetMemberByIdAsync(memberId);
            if(member==null) return NotFound();
            member.DisplayName=updateProfileDTO.displayname ?? member.DisplayName;
             member.Description=updateProfileDTO.Description ?? member.Description;
              member.City=updateProfileDTO.City ?? member.City;
               member.Country=updateProfileDTO.Country ?? member.Country;
                memberRepo.UpdateMemberAsync(member);
                if(await repository.SaveChangesAsync())
            {
                return NoContent();
            }
            return BadRequest();
                 
            
        }
    }
    
}
