using System;
using Api.DTOs;
using Api.Entities;
using Api.Interface;
using Api.Services;

namespace Api.Extensions;

public static class UserExtention
{
    public static UserDTO ToDTO(this User user, ITokenService tokenService)
    {
        var UserDTO = new UserDTO
        {
            UserId = user.UserId,
            Name = user.Name,
             ImageURl=user.ImageURl,
            Email = user.Email,
            Token = tokenService.CreateToken(user)

        };
        return UserDTO;


    }

}
