using System;
using System.ComponentModel.DataAnnotations;

namespace Api.DTOs;

public class RegisterDTO
{
    [Required]
    public string Name { get; set; } = "";
    [Required]
    [EmailAddress]
    public string Email { get; set; }= "";
    [Required]
    [Length(10,16)]
    public string Password { get; set; } = "";

}
