using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Fabrit.Heroes.Web.Hubs
{
    public class ChatHub : Hub                                            
    {
        public Task SendMessage1(string user, string message, string avatarPath, string hour)              
        {
            return Clients.All.SendAsync("ReceiveOne", user, message, avatarPath, hour);    
        }
    }
}
