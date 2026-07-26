using System.Security.Claims;
using Api.Data.Repositories;
using Api.DTOs;
using Api.Entities;
using Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Api.SignalR
{
    [Authorize]
    public class MessageHub(IMessageRepo messageRepo,
    IMemberRepo memberRepo):Hub
    
    {
        public override async Task OnConnectedAsync()
        {
        var httpcontext=Context.GetHttpContext();
        var otherUser=httpcontext?.Request?.Query["UserId"].ToString() ?? throw new HubException("no user Id");
        var GroupName=CreateGroupName(GetUserId(),otherUser);
        await Groups.AddToGroupAsync(Context.ConnectionId,GroupName);
        var message=await messageRepo.GetMessageThread(GetUserId(),otherUser);
        await Clients.Group(GroupName).SendAsync("ReceiveMessageThread",message);
        }
        public async Task SendMessage(CreateMessageDTO createMessageDTO)
        {
            var CurrentJWTMemberId = GetUserId();
            var sender = await memberRepo.GetMemberByIdAsync(CurrentJWTMemberId);
            var recipient = await memberRepo.GetMemberByIdAsync(createMessageDTO.RecipientId);
            if (recipient == null ||
                sender == null ||
                sender.Id == createMessageDTO.RecipientId)
            {
            throw new HubException("Cannot send");
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
                var GroupName=CreateGroupName(sender.Id,recipient.Id);
                await Clients.Group(GroupName).SendAsync("NewMessage",message.ToDto());
                 
            }
        }




        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        private  static string CreateGroupName(string caller,string other)
        {
         var stringCompare=string.CompareOrdinal(caller,other) < 0;
         return stringCompare ? $"{caller}-{other}": $"{other}-{caller}";
        }



        private string GetUserId()
        {
            return Context.User?.FindFirstValue(ClaimTypes.NameIdentifier)?? throw new Exception("no user id");
        }
    }
    
}