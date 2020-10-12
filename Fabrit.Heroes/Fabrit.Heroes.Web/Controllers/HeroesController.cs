using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data.Business.Hero;
using Fabrit.Heroes.Data.Entities.User;
using Fabrit.Heroes.Infrastructure.Common;
using Fabrit.Heroes.Web.Infrastructure.Controller;
using Fabrit.Heroes.Web.Infrastructure.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Fabrit.Heroes.Web.Authorization;

namespace Fabrit.Heroes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ApiController
    {
        private readonly IHeroService _heroService;

        public HeroesController(IHeroService heroService)
        {
            _heroService = heroService;
        }

        [HttpGet]
        [AuthorizeUserCustom(RoleType.General)]
        public IActionResult GetAll()
        {
            return Ok(_heroService.GetAll());
        }

        [HttpGet("getAllPlayers")]
        [AuthorizeUserCustom(RoleType.General)]
        public IActionResult GetAllPlayers()
        {
            return Ok(_heroService.GetAllPlayers());
        }

        [HttpGet("sortedPowers")]
        [AuthorizeUserCustom(RoleType.General)]
        public IActionResult GetAllOrderedPowers()
        {
            return Ok(_heroService.GetAllHeroesWithPowers());
        }

        [HttpDelete("{id}")]
        [AuthorizeUserCustom(RoleType.Admin)]

        public async Task<IActionResult> DeleteById(int id)
        {
            await _heroService.DeleteHeroById(id);
            return NoContent();
        }

        [HttpGet("entityTypeDto/{id}")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> GetEntityTypeDto(int id)
        {
           return Ok( await _heroService.GetEntityTypeDto(id));
        }

        [HttpGet("heroesRowData")]
        [AuthorizeUserCustom(RoleType.General)]
        public IActionResult GetHeroesRowData()
        {
            return Ok(_heroService.GetHeroRowData());
        }

        [HttpGet("adminMapRowData")]
        [AuthorizeUserCustom(RoleType.General)]
        public IActionResult GetAdminMapRowData()
        {
            return Ok(_heroService.GetAdminMapRowData());
        }

        [HttpGet("{id}")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _heroService.FindHeroDtoById(id));
        }

        [HttpPost]
        [AuthorizeUserCustom(RoleType.Admin)]
        public async Task<IActionResult> CreateHero([FromBody] HeroDto heroDto)
        {
            await _heroService.CreateHero(heroDto);
            return Created(Constants.HTTP_CREATED, heroDto);
        }

        [HttpPut]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> UpdateHero([FromBody] HeroDto heroDto)
        {
            await _heroService.UpdateHero(heroDto);
            return Created(Constants.HTTP_UPDATED, heroDto);
        }

        [HttpPut("avatar")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> ChangeHeroAvatar([FromQuery] int id, [FromQuery] string avatarPath)
        {
            await _heroService.ChangeHeroAvatar(id, avatarPath);
            return Ok();
        }

        [HttpPut("location")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> ChangeHeroLocation([FromQuery] int id, [FromQuery] double latitude, [FromQuery] double longitude)
        {
            await _heroService.ChangeHeroLocation(id, latitude, longitude);
            return Ok();
        }


        [HttpPut("travel")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> ChangeHeroTravel([FromQuery] int id, [FromQuery] double latitude, [FromQuery] double longitude)
        {
            return Ok(await _heroService.ChangeHeroTravel(id, latitude, longitude));
        }


        [HttpPut("birthday")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> UpdateHeroBirthday([FromQuery] int id, [FromQuery] string birthday)
        {
            await _heroService.UpdateHeroBirthday(id, birthday);
            return Ok();
        }

        [HttpGet("user/{id}")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> GetHeroByUserId(int id)
        {
            return Ok(await _heroService.GetHeroByUserId(id));
        }

        [HttpPut("details")]
        [AuthorizeUserCustom(RoleType.Regular)]
        public async Task<IActionResult> UpdateEntityDetails(HeroDto entity)
        {
            await _heroService.ChangeEntityDetails(entity);
            return Ok(entity);
        }

        [HttpGet("heroesInRange/{id}")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> GetHeroesInRangeAsync(int id)
        {
            return Ok(await _heroService.GetHeroesInRange(id));
        }
    }
}
