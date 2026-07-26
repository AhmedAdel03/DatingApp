using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Api.SignalR
{
    public class PresenceHub(PresenceTracker presenceTracker):Hub
    {
        [Authorize]
        public override async Task OnConnectedAsync()
        {
            await presenceTracker.UserConnected(GetUserId(),Context.ConnectionId);
            await Clients.Others.SendAsync("UserOnline",GetUserId());
            var CurrentConnectedUsers= await presenceTracker.GetOnlineUsers();
            await Clients.All.SendAsync("GetOnlineUsers",CurrentConnectedUsers); 
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
          await Clients.Others.SendAsync("UserOffline",GetUserId());
            await presenceTracker.UserDisconnected(GetUserId(),Context.ConnectionId);

          var CurrentConnectedUsers= await presenceTracker.GetOnlineUsers();
            await Clients.All.SendAsync("GetOnlineUsers",CurrentConnectedUsers); 
            await base.OnDisconnectedAsync(exception);
        }
        private string GetUserId()
        {
            return Context.User?.FindFirstValue(ClaimTypes.NameIdentifier)?? throw new Exception("no user id");
        }
    }
    
}