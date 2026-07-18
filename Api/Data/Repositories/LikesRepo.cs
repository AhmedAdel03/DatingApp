using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class LikesRepo(AppDbContext _context) : ILikesRepo
    {
        public void AddLike(MemberLikes like)
        {
            _context.Likes.Add(like);
        }

        public void Delete(MemberLikes like)
        {
            _context.Likes.Remove(like);
        }

        public async Task<IReadOnlyList<string>> GetCurrentMemberLikesId(string memberId)
        {
            return await _context.Likes.Where(x=>x.SourceMemberId==memberId)
            .Select(x=>x.TargetMemberId).ToListAsync();
        }

        public async Task<MemberLikes?> GetMemberLike(string sourceMemberId, string targetMemberId)
        {
            return await _context.Likes.FindAsync(sourceMemberId,targetMemberId);
        }

        public async Task<IReadOnlyList<Member>> GetMemberLikes(string predicate, string memberId)
        {
            var query=_context.Likes.AsQueryable();
            switch (predicate)
            {
                case "Liked":
                return await query.Where(x=>x.SourceMemberId==memberId).Select(x=>x.TargetMember).ToListAsync();
                case"LikedBy":
                return await query.Where(x=>x.TargetMemberId==memberId).Select(x=>x.SourceMember).ToListAsync();
                
                default: 
                var likeIds = await GetCurrentMemberLikesId(memberId);
                return await query.Where(x=>x.TargetMemberId==memberId&&likeIds.Contains(x.SourceMemberId)).Select(x=>x.SourceMember).ToListAsync();

            }
        }

        public async Task<bool> SaveChanges()
        {
        return await _context.SaveChangesAsync()>0;

        }
    }
}