using System.Collections.Concurrent;

namespace Api.SignalR
{
    public class PresenceTracker
    {
        private readonly ConcurrentDictionary<string,ConcurrentDictionary<string,byte>>OnlineUsers=new ();
        public Task UserConnected(string UserId,string ConnectionId)
        {
            var connections=OnlineUsers.GetOrAdd(UserId,_=>new ConcurrentDictionary<string, byte>());
            connections.TryAdd(ConnectionId,0);
            return Task.CompletedTask;
        }
         public Task UserDisconnected(string UserId,string ConnectionId)
        {
            if(OnlineUsers.TryGetValue(UserId,out var connections))
            {
                connections.TryRemove(ConnectionId,out _);
                   if(connections.IsEmpty)
                {
                    OnlineUsers.TryRemove(UserId,out _);
                }
            }
           return Task.CompletedTask;
        }
        public Task<string[]>GetOnlineUsers()
        {
            return Task.FromResult(OnlineUsers.Keys.OrderBy(k=>k).ToArray());
        }


    }
}