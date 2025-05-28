using Microsoft.AspNetCore.SignalR;

namespace MangoApi.Hubs
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, string> Users = new();

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }



        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }


    }
}
