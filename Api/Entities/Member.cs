using System;
using System.Text.Json.Serialization;

namespace Api.Entities;

public class Member
{
    public string Id { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public string? ImageUrl { get; set; }
    public required string DisplayName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public required string Gender { get; set; } = null!;

    public string Description { get; set; } = null!;
    public required string City { get; set; }
    public required string Country { get; set; }
    //Nav
    [JsonIgnore]
    public List<MemberLikes>LikesIRecived { get; set; }=[];
    public List<MemberLikes>LikesISent { get; set; }=[];
    public List<Message>MessagesRecived { get; set; }=[];
    public List<Message>MessagesSent { get; set; }=[];

    public List<Photo> Photos { get; set; } = [];
    public User User { get; set; } = null!;

}
