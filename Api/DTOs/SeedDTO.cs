using System;

namespace Api.DTOs;
/* this class made to seed a DB with some data to make testing easy and 
also this entity represent the actual member on the app*/

public class SeedDTO
{
    internal string displayname;

    public string Id { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public required string Email { get; set; }
    public string? ImageUrl { get; set; }
    public required string DisplayName { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime LastActive { get; set; } 
    public required string Gender { get; set; } = null!;

    public string Description { get; set; } = null!;
    public required string City { get; set; }
    public required string Country { get; set; }
    

}
