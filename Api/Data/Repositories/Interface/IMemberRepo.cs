using System;
using Api.Data.Repositories;
using Api.Entities;
using Api.Helpers;

namespace Api.Services.Interface;

public interface IMemberRepo 
{
    public void AddmemberAsync();
     public Task<Member?> GetMemberForUpdate(string id);
     public Task<PaginatedResult<Member>>GetMembersAsync(MemberParams MemberParams);
    public Task<Member> GetMemberByIdAsync(string id);
    public Task<IReadOnlyList<Photo>>GetMemberPhotosAsync(string id);
}
