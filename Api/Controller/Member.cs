using System.Security.Claims;
using Api.Data.Repositories;
using Api.DTOs;
using Api.Entities;
using Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Member(IMemberRepo memberRepo, IRepository<Member> repository, IPhotoService photoService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            var Data = await memberRepo.GetMembersAsync();
            if (Data.Any())
                return Ok(Data);
            return NotFound();

        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Getmember(string id)
        {
            var member = await memberRepo.GetMemberByIdAsync(id);
            if (member == null)
                return NotFound();
            return Ok(member);
        }
        [HttpGet("{id}/photo")]
        public async Task<ActionResult> GetmemberPhoto(string id)
        {
            var member = await memberRepo.GetMemberPhotosAsync(id);
            if (member == null)
                return NotFound();
            return Ok(member);
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateMember(UpdateProfileDTO updateProfileDTO)
        {
            var memberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (memberId == null) return NotFound();
            var member = await memberRepo.GetMemberByIdAsync(memberId);
            if (member == null) return NotFound();
            member.DisplayName = updateProfileDTO.displayname ?? member.DisplayName;
            member.Description = updateProfileDTO.Description ?? member.Description;
            member.City = updateProfileDTO.City ?? member.City;
            member.Country = updateProfileDTO.Country ?? member.Country;
            if (await repository.SaveChangesAsync())
            {
                return NoContent();
            }
            return BadRequest();



        }
        [HttpPost("UploadPhoto")]
      public async Task<ActionResult<Photo>>UploadPhoto(IFormFile file)
        {
            var memberId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(memberId==null) return NotFound();
            var member= await memberRepo.GetMemberForUpdate(memberId);
             if(member==null) return NotFound("member not found");
           var UploadedPhoto= await photoService.UploadImageasync(file);
           if(UploadedPhoto.Error!=null) return BadRequest();            
            var photo= new Photo
                {
                    PhotoUrl=UploadedPhoto.Url.AbsoluteUri,
                    PhotoPublicId=UploadedPhoto.PublicId,
                    Memberid=memberId

                };
                
            if(member.ImageUrl==null)
            {
                member.ImageUrl=UploadedPhoto.Url.AbsoluteUri;
                 member.User.ImageURl=UploadedPhoto.Url.AbsoluteUri;
            }
            member.Photos.Add(photo);
               if (await repository.SaveChangesAsync())
            {
                return Ok("upload Success");
            }
            return BadRequest("problem adding photo");
            
        }

    }



}
