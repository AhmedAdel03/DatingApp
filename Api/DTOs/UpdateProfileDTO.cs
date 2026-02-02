using System;
using System.ComponentModel.DataAnnotations;

namespace Api.DTOs;

public class UpdateProfileDTO
{    
    [MinLength(4)]
    public string? displayname { get; set; }
        [MinLength(4)]

    public string? Description { get; set; }
        [MinLength(4)]

    public string? City { get; set; }
        [MinLength(4)]

    public string? Country { get; set; }


}
