using System;
using System.ComponentModel.DataAnnotations;

namespace Api.DTOs;

public class UpdateProfileDTO
{

    public string displayname { get; set; }

    public string? Description { get; set; }

    public string City { get; set; }

    public string Country { get; set; }


}
