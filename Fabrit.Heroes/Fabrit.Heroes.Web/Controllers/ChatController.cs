using Fabrit.Heroes.Data.Business.Message;
using Fabrit.Heroes.Data.Entities.User;
using Fabrit.Heroes.Web.Authorization;
using Fabrit.Heroes.Web.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Fabrit.Heroes.Web.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [Route("send")]                                          
        [HttpPost]
        [AuthorizeUserCustom(RoleType.General)]
        public IActionResult SendRequest([FromBody] MessageDto message)
        {
            _hubContext.Clients.All.SendAsync("ReceiveOne", message.User, message.Text, message.AvatarPath, message.Hour);
            return Ok();
        }
    }
}
