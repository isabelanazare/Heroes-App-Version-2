using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data;
using Fabrit.Heroes.Data.Entities.User;
using Fabrit.Heroes.Infrastructure.Common;
using Fabrit.Heroes.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Business.Services
{
    public class FileService : IFileService
    {
        private readonly IUserService _userService;
        private readonly IHeroService _heroService;

        public FileService(IUserService userService, IHeroService heroService)
        {
            _userService = userService;
            _heroService = heroService;
        }

        public async Task<string> UploadAvatar(IFormFile photoFile, int id, bool isUser)
        {
            await CheckUploatedPhoto(photoFile, id, isUser);
            try
            {
                CreateAvatarPath(photoFile, isUser, id, out string fullPath, out string dbPath);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await photoFile.CopyToAsync(stream);
                }

                return Constants.APP_URL + dbPath;
            }
            catch (Exception e)
            {
                throw new InvalidFileException(e.Message);
            }
        }

        private void CreateAvatarPath(IFormFile photoFile, bool isUserPhoto, int id, out string fullPath, out string dbPath)
        {
            var folderName = isUserPhoto ? Path.Combine(Constants.IMAGES_DIRECTORY, Constants.USERS_DIRECTORY) : Path.Combine(Constants.IMAGES_DIRECTORY, Constants.HEROES_DIRECTORY);
            folderName = Path.Combine(folderName, id.ToString());
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }

            var fileName = ContentDispositionHeaderValue.Parse(photoFile.ContentDisposition).FileName.Trim(Constants.QUOTATION_MARK);
            fullPath = Path.Combine(pathToSave, fileName);
            dbPath = Path.Combine(folderName, fileName);
        }

        private async Task CheckUploatedPhoto(IFormFile photoFile, int id, bool isUser)
        {
            if (photoFile.Length < 0 || !Constants.acceptedImageTypes.Contains(photoFile?.ContentType))
            {
                throw new InvalidFileException();
            }

            if (isUser)
            {
                await _userService.FindById(id);
            }
            else 
            { 
                await _heroService.FindById(id);
            }
        }
    }
}
