using System;
using System.ComponentModel.DataAnnotations;

namespace Api.DTOs;

public class LoginDTO
{
    [EmailAddress]
    public string Email { get; set; } = "";
    [Length(10,16)]
    public string Password { get; set; } = "";
    

}
