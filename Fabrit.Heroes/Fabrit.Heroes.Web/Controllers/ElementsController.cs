using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fabrit.Heroes.Business.Services.Contracts;
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
    public class ElementsController : ApiController
    {
        private readonly IElementService _elementsService;

        public ElementsController(IElementService elementService)
        {
            _elementsService = elementService;
        }

        [HttpGet]
        [AuthorizeUserCustom(RoleType.General)]
        public IActionResult GetElements()
        {
            return Ok(_elementsService.GetAll());
        }
    }
}
