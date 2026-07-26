using System.Security.Claims;
using Api.Data.Repositories;
using Api.DTOs;
using Api.Entities;
using Api.Helpers;
using Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController(IMessageRepo messageRepo, IMemberRepo memberRepo) : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<MessageDTO>> CreateMessage(CreateMessageDTO createMessageDTO)
        {
            var CurrentJWTMemberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sender = await memberRepo.GetMemberByIdAsync(CurrentJWTMemberId);
            var recipient = await memberRepo.GetMemberByIdAsync(createMessageDTO.RecipientId);
            if (recipient == null ||
                sender == null ||
                sender.Id == createMessageDTO.RecipientId)
            {
                return BadRequest("Cannot send this message");
            }
            var message = new Message
            {
                SenderId = sender.Id,
                RecipientId = recipient.Id,
                Content = createMessageDTO.Content
            };
            messageRepo.AddMessage(message);
            if (await messageRepo.SaveChangesAsync())
            {
                return Ok(MessageExtention.ToDto(message));
            }
            return BadRequest();

        }
        [Authorize]

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<MessageDTO>>> GetMessageContainer([FromQuery] MessageParams messageParams)
        {
            messageParams.MemberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await messageRepo.GetMessagesForMember(messageParams));
        }
        [Authorize]

        [HttpGet("Thread/{RecipientId}")]
        public async Task<ActionResult<PaginatedResult<MessageDTO>>> GetMessagesThread(string RecipientId)
        {
            var CurrentMemberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (CurrentMemberId == null) return BadRequest();
            return Ok(await messageRepo.GetMessageThread(CurrentMemberId, RecipientId));

        }
        [Authorize]

        [HttpDelete("{MessageId}")]
        public async Task<ActionResult> DeleteMessage(string MessageId)
        {
            var MemberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var message = await messageRepo.Getmessage(MessageId);
            if (IsEligibleToDeleteMessage(MemberId, message))
            {
                message.SenderDeleted = true;
                message.RecipientDeleted = true;
                if (await messageRepo.SaveChangesAsync())
                {
                    return Ok();
                }
                return BadRequest();
            }
            return BadRequest();

        }
        private bool IsEligibleToDeleteMessage(string memberId, Message? message)
        {
            return message != null &&
                   (message.SenderId == memberId || message.RecipientId == memberId);
        }

    }
}
