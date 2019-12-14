namespace SignalRChat.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using System.Threading.Tasks;

    public class ChatHub : Hub
    {
        public async Task SendMessageAsync(string user, string message)
        {
            await this.Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
