using System;

namespace Api.Entities;
 
public class User
{
    public string UserId { get; set; } = Guid.NewGuid().ToString(); //guid for auto generate Id For Users
    public required string Name { get; set; }
    public required string Email { get; set; }
    
   public string? ImageURl { get; set; }

    public required byte[] PassWordHash { get; set; }
    public required byte[] PassWordSalt { get; set; }
    //Nav
    public Member Member { get; set; } = null!;

}
