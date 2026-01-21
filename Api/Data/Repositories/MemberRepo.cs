using System;
using Api.Data;
using Api.Data.Repositories;
using Api.Entities;
using Api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class MemberRepo : Repository<Member>, IMemberRepo
{
    private readonly AppDbContext _Context;
    public MemberRepo(AppDbContext context) : base(context)
    {
        _Context = context;
    }

    public async Task<IReadOnlyList<Photo>> GetAllPhotos(string MemberId)
    {
        return await _Context.Members.
        Where(x => x.Id == MemberId)
        .SelectMany(x => x.Photos).ToListAsync();
    }
     
}
