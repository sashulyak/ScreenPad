using Microsoft.AspNet.SignalR;

namespace ScreenPad.WebUI.Hubs
{
	public class GameHub : Hub
	{
        public void Send(string message, short id)
		{
			// Call the addNewMessageToPage method to update clients.
			Clients.All.addNewMessageToPage(message, id);
        }
	}
}