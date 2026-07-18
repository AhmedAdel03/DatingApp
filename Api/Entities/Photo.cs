using System;
using System.Text.Json.Serialization;

namespace Api.Entities;

public class Photo
{
    public int PhotoId { get; set; }
    public required string PhotoUrl { get; set; }
    public string PhotoPublicId { get; set; }
    public string Memberid { get; set; } = null!;
    //Nav
    [JsonIgnore]
    public Member Member { get; set; } = null!;

}
