
using System.Security.Claims;
using Api.Data.Repositories;
using Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
 

namespace Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController(ILikesRepo likesRepo) : ControllerBase
    {
        [Authorize]
        [HttpPost("{TargetMemberId}")]
        public async Task<ActionResult>ToggleLike(string TargetMemberId)
        {
            var SourcememberId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            var LikeExist=await likesRepo.GetMemberLike(SourcememberId,TargetMemberId);
            if(LikeExist==null)
            {
                var newLike= new MemberLikes
                {
                    SourceMemberId=SourcememberId,
                    TargetMemberId=TargetMemberId
                    
                };
                likesRepo.AddLike(newLike);
            }
            else
            {
                likesRepo.Delete(LikeExist);
            }
            if(await likesRepo.SaveChanges())
            {
                return Ok();
            }
            else return BadRequest();


        }
                [Authorize]

        [HttpGet("list")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetCurrentMemberLikesIds()
        {
            var SourcememberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await likesRepo.GetCurrentMemberLikesId(SourcememberId));

        }

                [Authorize]

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Member>>> GetMemberLikes([FromQuery]string predicate)
        {
            var SourcememberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var members=await likesRepo.GetMemberLikes(predicate,SourcememberId);
            return Ok(members);
            
        }


    }
}
