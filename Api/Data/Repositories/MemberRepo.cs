using System;
using Api.Data;
using Api.Data.Repositories;
using Api.Entities;
using Api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class MemberRepo : IMemberRepo
{
    private readonly AppDbContext _Context;
    public MemberRepo(AppDbContext context)  
    {
        _Context = context;
    }
    
    // 
    public void AddmemberAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Member> GetMemberByIdAsync(string id)
    {
       return await _Context.Members.FindAsync(id);
    }

    public async Task<IReadOnlyList<Photo>> GetMemberPhotosAsync(string id)
    {
     return await _Context.Photos
    .Where(p => p.Memberid == id)
    .ToListAsync();
    }

    public async Task<IReadOnlyList<Member>> GetMembersAsync()
    {
      return await _Context.Members.ToListAsync();
    }

    public void UpdateMemberAsync(Member member)
    {
        throw new NotImplementedException();
    }
}
