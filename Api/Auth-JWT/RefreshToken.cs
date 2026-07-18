using System;

namespace Api.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public required string Token { get; set; }

    public DateTime ExpireDate { get; set; }=DateTime.UtcNow.AddDays(30);
    public required bool IsRevoked { get; set; }
    public required string  UserID { get; set; }
    public DateTime CreatedAt  {get;set; }
    //Nav
    public   User User { get; set; }

}
