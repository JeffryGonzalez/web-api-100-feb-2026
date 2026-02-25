using SoftwareShared.Notifications;

namespace Software.Api.Clients;

public class NotificationsApi(HttpClient client)
{

    public async Task SendNotification(NotificationRequest request)
    {
        var response = await client.PostAsJsonAsync("/notifications", request);
        response.EnsureSuccessStatusCode();
    }
}
