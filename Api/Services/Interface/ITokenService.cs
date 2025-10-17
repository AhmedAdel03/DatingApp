using System;
using Api.Entities;

namespace Api.Interface;

public interface ITokenService
{
    string CreateToken(User user);

}
