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
        var member=await _Context.Members.FindAsync(id);
        if(member==null) return null;
       return member;
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
         _Context.Entry(member).State=EntityState.Modified;
    }
}
