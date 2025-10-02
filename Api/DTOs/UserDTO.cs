using System;

namespace Api.DTOs;

public class UserDTO
{
    public required string UserId { get; set; }
    public required string Email { get; set; }

    public required string Name { get; set; }
    public string? ImageURl { get; set; }
    public required string Token { get; set; }


}
