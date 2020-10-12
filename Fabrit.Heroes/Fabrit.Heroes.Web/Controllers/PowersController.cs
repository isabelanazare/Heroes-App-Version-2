using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data.Business.Power;
using Fabrit.Heroes.Data.Entities.User;
using Fabrit.Heroes.Infrastructure.Common;
using Fabrit.Heroes.Web.Authorization;
using Fabrit.Heroes.Web.Infrastructure.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Web.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class PowersController : ApiController
    {
        private readonly IPowerService _powerService;

        public PowersController(IPowerService powerService)
        {
            _powerService = powerService;
        }

        [HttpGet("tableData")]
        [AuthorizeUserCustom(RoleType.General)]
        public IActionResult GetPowersTableData()
        {
            return Ok(_powerService.GetPowersTableData());
        }

        [HttpGet("names")]
        [AuthorizeUserCustom(RoleType.General)]
        public IActionResult GetPowersName()
        {
            return Ok(_powerService.GetPowersName());
        }

        [HttpGet("{id}")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _powerService.FindPowerDtoById(id));
        }

        [HttpPost]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> CreatePower([FromBody] PowerDto powerDto)
        {
            await _powerService.CreatePower(powerDto);
            return Created(Constants.HTTP_CREATED, powerDto);
        }

        [HttpDelete("{id}")]
        [AuthorizeUserCustom(RoleType.General)]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _powerService.DeletePowerById(id);
            return NoContent();
        }

        [HttpGet]
        [AuthorizeUserCustom(RoleType.General)]
        public IActionResult GetAll()
        {
            return Ok(_powerService.GetAll());
        }

        [HttpPut]
        [AuthorizeUserCustom(RoleType.Admin)]
        public async Task<IActionResult> UpdatePower([FromBody] PowerDto powerDto)
        {
            await _powerService.UpdatePower(powerDto);
            return Created(Constants.HTTP_UPDATED, powerDto);
        }
    }
}
