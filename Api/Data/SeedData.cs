using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Api.DTOs;
using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class SeedData
{
   public static async Task SeedDataFromFile(AppDbContext context)
    {
        if (await context.Users.AnyAsync()) return;
        var memberdata = await File.ReadAllTextAsync("Data/UserSeedData.json");
        var members = JsonSerializer.Deserialize<List<SeedDTO>>(memberdata);
        if (members == null) return;                                                 
        using var hmac = new HMACSHA512();
        foreach (var member in members)
        {
            Console.WriteLine("🌱 Seeding started...");
            var user = new User
            {
                UserId = member.Id,
                Email = member.Email,
                PassWordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Eldola1234531")),
                PassWordSalt = hmac.Key,
                Name = member.DisplayName,
                Member = new Member
                {
                    Id = member.Id,
                    DateOfBirth = member.DateOfBirth,
                    ImageUrl = member.ImageUrl,
                    CreatedAt = member.CreatedAt,
                    LastActive = member.LastActive,
                    Description = member.Description,
                    Gender = member.Gender,
                    City = member.City,
                    Country = member.Country,
                    DisplayName = member.DisplayName
                }

            };
            user.Member.Photos.Add(new Photo
            {
                PhotoUrl = member.ImageUrl!,
                Memberid = member.Id
            });

            context.Users.Add(user);

        }
await context.SaveChangesAsync();
    }

}
