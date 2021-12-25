using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebApplication;

namespace WebApplication
{
    public class ChatHub : Hub
    {
        public async Task Send(string message, string name)
        {
            await this.Clients.All.SendAsync("Send", message, name);

            await Task.Run(() =>
            {
                using (ChatContext db = new ChatContext())
                {
                    Message msgToServ = new Message {Message1 = message, Username = name};
                    db.Messages.Add(msgToServ);
                    db.SaveChanges();
                }
            });
        }
    }
}