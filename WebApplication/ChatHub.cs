using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebApplication
{
    public class ChatHub : Hub
    {
        public async Task Send(string message, string name)
        {
            await this.Clients.All.SendAsync("Send", message, name);
        }
    }
}