using System;

namespace Api.Error;

public class ApiException(int StatusCode,string message,string ? Details)
{
    public int StatusCode { get; set; } = StatusCode;
    public string message { get; set; } = message;
    public string? Details { get; set; } = Details;


}
