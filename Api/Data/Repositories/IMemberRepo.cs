using System;
using Api.Data.Repositories;
using Api.Entities;

namespace Api.Services.Interface;

public interface IMemberRepo:IRepository<Member>
{
    Task<IReadOnlyList<Photo>> GetAllPhotos(string MemberId);
}
