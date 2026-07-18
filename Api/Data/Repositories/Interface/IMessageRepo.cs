using Api.DTOs;
using Api.Entities;
using Api.Helpers;

namespace Api.Data.Repositories
{
    public interface IMessageRepo
    {
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message?>Getmessage(string messageId);
        Task<PaginatedResult<MessageDTO>>GetMessagesForMember(MessageParams messageParams);
        Task<IReadOnlyList<MessageDTO>>GetMessageThread(string CurrentMemberId,string RecipientId);
        Task<bool>SaveChangesAsync();
    }
    
}