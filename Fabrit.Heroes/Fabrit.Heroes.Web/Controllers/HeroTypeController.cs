using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data.Business.Element;
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
    public class HeroTypeController : ApiController
    {
        private readonly IHeroTypeService _heroTypeService;

        public HeroTypeController(IHeroTypeService heroTypeService)
        {
            _heroTypeService = heroTypeService;
        }

        [HttpGet]
        [AuthorizeUserCustom(RoleType.General)]
        public IActionResult GetAll()
        {
            return Ok(_heroTypeService.GetAll());
        }
    }
}
