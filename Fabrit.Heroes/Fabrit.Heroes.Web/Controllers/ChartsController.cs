using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data.Business.Hero;
using Fabrit.Heroes.Data.Entities.User;
using Fabrit.Heroes.Web.Authorization;
using Fabrit.Heroes.Web.Infrastructure.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fabrit.Heroes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ApiController
    {
        private readonly IChartService _chartService;

        public ChartsController(IChartService chartService)
        {
            _chartService = chartService;
        }

        [HttpGet("heroes")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> GetHeroChartData()
        {
            return Ok(await _chartService.GetHeroChartData(ChartType.XAxisHero));
        }

        [HttpGet("powers")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> GetPowerChartData()
        {
            return Ok(await _chartService.GetHeroChartData(ChartType.XAxisPower));
        }
    }
}
