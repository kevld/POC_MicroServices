using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace POC_MicroServices.Mail.Hub
{
    public class NotificationHub : DynamicHub
    {
        private readonly IHubContext<NotificationHub> _hubContext;


        public static List<string> connectedUsers = new List<string>();


        public NotificationHub(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Users");


            int cpt = connectedUsers.Count;
            var connId = Context.ConnectionId;
            if (!connectedUsers.Contains(connId))
            {
                connectedUsers.Add(connId);
            }



            await base.OnConnectedAsync();

            //await SendStatuses();
        }


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Users");

            connectedUsers.Remove(Context.ConnectionId);

            if (connectedUsers.Count == 0)
            {

            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendNotification()
        {
            await _hubContext.Clients.All.SendAsync("notification", "Notification : Mail envoyé");

        }
    }
}
