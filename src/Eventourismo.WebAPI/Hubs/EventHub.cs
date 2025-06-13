using Microsoft.AspNetCore.SignalR;

namespace Eventourismo.WebAPI.Hubs;

public class EventHub : Hub
{
    public async Task JoinEventGroup(string eventId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"Event_{eventId}");
    }

    public async Task LeaveEventGroup(string eventId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Event_{eventId}");
    }

    public async Task SendEventUpdate(string eventId, string message)
    {
        await Clients.Group($"Event_{eventId}").SendAsync("EventUpdate", message);
    }
}