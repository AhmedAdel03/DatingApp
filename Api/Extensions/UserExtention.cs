using System;
using Api.DTOs;
using Api.Entities;
using Api.Interface;
using Api.Services;

namespace Api.Extensions;

public static class UserExtention
{
    public static async Task<UserDTO> ToDTO(this User user, ITokenService tokenService)
    {
        var token=await tokenService.CreateToken(user);
        var UserDTO = new UserDTO
        {
            UserId = user.UserId,
            Name = user.Name,
             ImageURl=user.ImageURl,
            Email = user.Email,
            AccessToken =token.AccessToken,
            RefreshToken=token.RefreshToken


        };
        return UserDTO;


    }

}
