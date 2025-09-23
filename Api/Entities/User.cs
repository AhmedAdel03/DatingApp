using System;

namespace Api.Entities;

public class User
{
    public string UserId { get; set; } = Guid.NewGuid().ToString(); //guid for auto generate Id For Users
    public required string Name { get; set; }
    public required string Email { get; set; }

}
