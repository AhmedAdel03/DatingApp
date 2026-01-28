using System;
using Api.Data.Repositories;
using Api.Entities;

namespace Api.Services.Interface;

public interface IMemberRepo 
{
    public void AddmemberAsync();
     public void UpdateMemberAsync(Member member);
     Task<IReadOnlyList<Member>>GetMembersAsync();
     Task<Member> GetMemberByIdAsync(string id);
     Task<IReadOnlyList<Photo>>GetMemberPhotosAsync(string id);
}
