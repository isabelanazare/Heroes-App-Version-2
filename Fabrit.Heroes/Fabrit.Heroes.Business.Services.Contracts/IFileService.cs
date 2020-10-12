using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Business.Services.Contracts
{
    public interface IFileService
    {
        Task<string> UploadAvatar(IFormFile photoFile, int id, bool isUser);
    }
}
