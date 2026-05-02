using System;
using System.ComponentModel.DataAnnotations;

namespace Api.DTOs;

public class RegisterDTO
{
     
    public string DisplayName { get; set; } = "";
  
    public string Email { get; set; }= "";
    public string Password { get; set; } = "";
    public required string Gender { get; set; }="";
    public required string City { get; set; }="";
    public required string Country { get; set; }="";
    public required DateOnly Dateofbirth { get; set; } 
     public string? Description { get; set; }="";



}
