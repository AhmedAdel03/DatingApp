using System;
using System.ComponentModel.DataAnnotations;

namespace Api.DTOs;

public class RegisterDTO
{
    [Required]
    public string DisplayName { get; set; } = "";
    [Required]
    [EmailAddress]
    public string Email { get; set; }= "";
    [Required]
    [Length(10,16)]
    public string Password { get; set; } = "";
     
    public required string Gender { get; set; }="";
    public required string City { get; set; }="";
    public required string Country { get; set; }="";
    public required DateOnly Dateofbirth { get; set; } 
        public required string Description { get; set; }="";



}
