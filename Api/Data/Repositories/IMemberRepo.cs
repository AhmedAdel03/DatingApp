using System;
using Api.Data.Repositories;
using Api.Entities;

namespace Api.Services.Interface;

public interface IMemberRepo 
{
    public void AddmemberAsync();
     public void UpdateMemberAsync(Member member);
     public Task<IReadOnlyList<Member>>GetMembersAsync();
    public Task<Member> GetMemberByIdAsync(string id);
    public Task<IReadOnlyList<Photo>>GetMemberPhotosAsync(string id);
}
