using System.Threading.Tasks;
using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data.Business.Battle;
using Fabrit.Heroes.Data.Entities.User;
using Fabrit.Heroes.Infrastructure.Common;
using Fabrit.Heroes.Web.Authorization;
using Fabrit.Heroes.Web.Infrastructure.Controller;
using Microsoft.AspNetCore.Mvc;

namespace Fabrit.Heroes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattleController : ApiController
    {
        private readonly IBattleService _battleService;


        public BattleController(IBattleService battleService)
        {
            _battleService = battleService;
        }

        [HttpGet]
        [AuthorizeUserCustom(RoleType.General)]
        public IActionResult GetAll()
        {
            return Ok(_battleService.GetAll());
        }

        [HttpPost("{intiatorId}/{opponentId}")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> CreateBattle(int intiatorId, int opponentId)
        {
            return Ok(await _battleService.CreateBattle(intiatorId, opponentId));
        }

        [HttpDelete("{id}")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _battleService.DeleteBattle(id);
            return NoContent();
        }

        [HttpPost("play")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> PlayGame([FromBody] BattleDto battleDto)
        {
            return Ok(await _battleService.PlayGame(battleDto));
        }

        [HttpGet("{id}/history")]
        [AuthorizeUserCustom(RoleType.General)]
        public IActionResult GetBattleHistory(int id)
        {
            return Ok(_battleService.GetBattleHistory(id));
        }
        
        [HttpGet("count/{id}")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> GetCountForHeroAsync(int id)
        {
            return Ok(await _battleService.GetNrOfBattlesForHero(id));
        }

        [HttpGet("badges/{id}")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> GetBadgesCount(int id)
        {
            return Ok(await _battleService.GetBadgesCountDto(id));
        }
    }
}
