using System;
using System.ComponentModel.DataAnnotations;

namespace Api.DTOs;

public class RefreshTokenDTo
{
   public required string RefreshToken { get; set; }

}
