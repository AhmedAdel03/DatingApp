using System;
using CloudinaryDotNet.Actions;

namespace Api.Services.Interface;

public interface IPhotoService
{
 Task<ImageUploadResult>UploadImageasync(IFormFile file);
 Task<DeletionResult> DeletionPhoto(string PublicId);
 
 }
