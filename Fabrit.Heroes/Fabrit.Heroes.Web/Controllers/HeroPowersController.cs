
using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data.Business.Hero;
using Fabrit.Heroes.Data.Business.Power;
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
    public class HeroPowersController : ApiController
    {
        private readonly IHeroPowerService _heroPowerService;

        public HeroPowersController(IHeroPowerService heroPowerService)
        {
            _heroPowerService = heroPowerService;
        }

        [HttpGet("{id}")]
        [AuthorizeUserCustom(RoleType.General)]
        public IActionResult GetById(int id)
        {
            return Ok(_heroPowerService.GetHeroPowersForHero(id));
        }

        [HttpGet("train/{id}")]
        [AuthorizeUserCustom(RoleType.Regular)]
        public async Task<IActionResult> TrainPowerAsync(int id)
        {
            await _heroPowerService.TrainPower(id);
            return Ok();
        }

        [HttpPost]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> ChangeMainPower(MainPowerChangeDto dto)
        {
            await _heroPowerService.ChangeMainPower(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [AuthorizeUserCustom(RoleType.Admin)]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _heroPowerService.DeletePowerById(id);
            return NoContent();
        }

        [HttpPost("powers")]
        [AuthorizeUserCustom(RoleType.Admin)]
        public async Task<IActionResult> AddPowers(VillainPowersDto dto)
        {
            await _heroPowerService.AddVillainPowers(dto);
            return Ok();
        }

        [HttpGet("power/{id}")]
        [AuthorizeUserCustom(RoleType.Admin)]
        public async Task<IActionResult> GetHeroPower(int id)
        {
            return Ok(await _heroPowerService.GetHeroPowerById(id));
        }

        [HttpPut("update")]
        [AuthorizeUserCustom(RoleType.Admin)]
        public async Task<IActionResult> UpdateHeroPower(HeroPowerDto dto)
        {
            await _heroPowerService.UpdateHeroPower(dto);
            return Ok();
        }
    }
}
