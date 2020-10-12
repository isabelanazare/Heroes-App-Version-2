
using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data.Entities.User;
using Fabrit.Heroes.Web.Authorization;
using Fabrit.Heroes.Web.Infrastructure.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fabrit.Heroes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ApiController
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("userAvatar"), DisableRequestSizeLimit]
        [AuthorizeUserCustom(RoleType.General)]
        public IActionResult UploadUserAvatar([FromQuery] int id)
        {
            var file = Request.Form.Files[0];
            return Ok(_fileService.UploadAvatar(file, id, true));
        }

        [HttpPost("heroAvatar"), DisableRequestSizeLimit]
        [AuthorizeUserCustom(RoleType.General)]
        public IActionResult UploadHeroAvatar([FromQuery] int id)
        {
            var file = Request.Form.Files[0];
            return Ok(_fileService.UploadAvatar(file, id, false));
        }
    }
}
