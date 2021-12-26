using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;
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

        public async Task Check(string login, string password)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Port=5432;Database=Chat;Username=postgres;Password=14072003"))
            {
                conn.Open();
               
            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT COUNT(*)::int FROM login WHERE log = @p1 AND password = @p2", conn))
                {
                 cmd.Parameters.AddWithValue("@p1", login);
                 cmd.Parameters.AddWithValue("@p2", password);
                 bool result = Convert.ToInt32(cmd.ExecuteScalar()) == 0 ? false : true;

                 await this.Clients.Caller.SendAsync("Check", result);
                }
            }
        }
    }
}