using System;
using Api.Data;
using Api.Data.Repositories;
using Api.Entities;
using Api.Helpers;
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

    public async Task<PaginatedResult<Member>> GetMembersAsync(MemberParams memberParams)
    {
        var query= _Context.Members.AsQueryable();
       query= query.Where(x=>x.Id!=memberParams.CurrentMemberId);
       if(memberParams.Gender!=null)
        {
          query=query.Where(x=>x.Gender==memberParams.Gender);
        }
 var minDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-memberParams.MaxAge - 1));

 var maxDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-memberParams.MinAge));

  query = query.Where(x => x.DateOfBirth > minDob && x.DateOfBirth <= maxDob);
  query= memberParams.OrderBy switch
  {
      "createdAt"=> query.OrderByDescending(x=>x.CreatedAt),
      _=> query.OrderByDescending(x=>x.LastActive)
  };


      return await paginationHelper.CreateAsync(query,memberParams.pageNumber,memberParams.PageSize);
    }

    public async Task<Member?> GetMemberForUpdate(string id)
    {
        return await _Context.Members
        .Include(x=>x.Photos)
        .Include(x=>x.User).SingleOrDefaultAsync(x=>x.Id==id);
    }
}
