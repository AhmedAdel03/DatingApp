using Api.DTOs;
using Api.Entities;
using Api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class MessageRepo(AppDbContext context) : IMessageRepo
    {
        public void AddMessage(Message message)
        {
            context.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            context.Messages.Remove(message);
        }

        public async Task<Message?> Getmessage(string messageId)
        {
           return await context.Messages.FirstOrDefaultAsync(x=>x.Id==messageId);
        }

        public async Task<PaginatedResult<MessageDTO>> GetMessagesForMember(MessageParams messageParams)
        {
            var query=context.Messages.OrderByDescending(x=>x.MessageSent).AsQueryable();
            query.Where(x=>x.SenderDeleted==false &&x.RecipientDeleted==false);
            query=messageParams.Container switch
            {
                "Outbox"=>query.Where(x=>x.SenderId==messageParams.MemberId),
                _=>query.Where(x=>x.RecipientId==messageParams.MemberId)
            };
            var MessageQuery=query.Select(MessageExtention.ToDTOProjection());
            return await paginationHelper.CreateAsync(MessageQuery,messageParams.pageNumber,messageParams.PageSize);
        }

        public async Task<IReadOnlyList<MessageDTO>> GetMessageThread(string CurrentMemberId, string RecipientId)
        {
            await context.Messages.Where(x=>x.RecipientId==CurrentMemberId&&x.SenderId==RecipientId&&x.DateRead==null)
            .ExecuteUpdateAsync(setters=>setters.SetProperty(x=>x.DateRead,DateTime.UtcNow));
            
            return await context.Messages
            .Where(x=>(x.RecipientId==CurrentMemberId&&x.RecipientDeleted==false &&x.SenderId==RecipientId)||
            (x.RecipientId==RecipientId&&x.SenderDeleted==false&&x.SenderId==CurrentMemberId))
            .Select(MessageExtention.ToDTOProjection()).ToListAsync();


        }

        public async Task<bool> SaveChangesAsync()
        {
         return await context.SaveChangesAsync() > 0;
        }
    }
}