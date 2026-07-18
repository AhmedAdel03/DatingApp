using System;
using Api.Helpers;
using Api.Services.Interface;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;

namespace Api.Services;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary _Cloudinary;
    public PhotoService(IOptions<CloudinarySettting> config)
    {
        var account=new Account(config.Value.CloudName,config.Value.ApiKey,config.Value.ApiSecret);
        _Cloudinary=new Cloudinary(account);
    }
    public async Task<DeletionResult> DeletionPhoto(string PublicId)
    {
         var deletionParams=new DeletionParams(PublicId);
         return await _Cloudinary.DestroyAsync(deletionParams);
    }

    public async Task<ImageUploadResult> UploadImageasync(IFormFile file)
    {
          var uploadedResult= new ImageUploadResult();
          if(file.Length>0)
        {
            await using var stream=file.OpenReadStream();
            var uploadParams=new ImageUploadParams
            {
                File=new FileDescription(file.FileName,stream),
                Transformation=new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                Folder="SweetMeet"

            };
            uploadedResult= await _Cloudinary.UploadAsync(uploadParams);
            
        }
        return uploadedResult;
        
}}
