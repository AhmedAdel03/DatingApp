using Api.Entities;

namespace Api.Data.Repositories
{
    public interface ILikesRepo
    {
        Task<MemberLikes?> GetMemberLike(string sourceMemberId, string targetMemberId);
        Task<IReadOnlyList<Member>>GetMemberLikes(string predicate,string memberId);
        Task<IReadOnlyList<string>>GetCurrentMemberLikesId(string memberId);
        void AddLike(MemberLikes like);
        void Delete(MemberLikes like);
        Task<bool> SaveChanges();


        
    }
    
}