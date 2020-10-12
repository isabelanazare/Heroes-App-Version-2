using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data.Business.Villain;
using Fabrit.Heroes.Data.Entities.User;
using Fabrit.Heroes.Web.Authorization;
using Fabrit.Heroes.Web.Infrastructure.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillainsController : ApiController
    {
        private readonly IVillainService _villainService;

        public VillainsController(IVillainService villainService)
        {
            _villainService = villainService;
        }

        [HttpGet]
        [AuthorizeUserCustom(RoleType.General)]
        public IActionResult GetAll()
        {
            return Ok(_villainService.GetAll());
        }

        [HttpGet("{id}")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _villainService.GetVillainDtoById(id));
        }

        [HttpGet("villainsRowData")]
        [AuthorizeUserCustom(RoleType.General)]
        public IActionResult GetHeroesRowData()
        {
            return Ok(_villainService.GetVillainRowData());
        }

        [HttpPut("avatar")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> ChangeHeroAvatarAsync([FromQuery] int id, [FromQuery] string avatarPath)
        {
            await _villainService.ChangeImage(id, avatarPath);
            return Ok();
        }

        [HttpPost]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> AddVillain(VillainDto villainDto)
        {
            var villain = await _villainService.AddVillain(villainDto);
            return Ok(villain);
        }

        [HttpPut]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> UpdateVillain(VillainDto villainDto)
        {
            await _villainService.UpdateVillain(villainDto);
            return Ok();
        }

        [AuthorizeUserCustom(RoleType.General)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVillainAsync(int id)
        {
            await _villainService.DeleteVillain(id);

            return NoContent();
        }

        [HttpPut("location")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> ChangeVillainLocation([FromQuery] int id, [FromQuery] double latitude, [FromQuery] double longitude)
        {
            await _villainService.ChangeVillainLocation(id, latitude, longitude);
            return Ok();
        }
    }
}
