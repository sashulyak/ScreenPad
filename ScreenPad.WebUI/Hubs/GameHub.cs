using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace ScreenPad.WebUI.Hubs
{
	public class GameHub : Hub
	{
        private readonly static IConnectionMapping<string> connections = new ConnectionMapping<string>();

        public void Send(string message, short id)
		{
			// Call the addNewMessageToPage method to update clients.
			Clients.All.addNewMessageToPage(message, id);
        }

	    public override Task OnConnected()
	    {
	        var connectionName = Context.QueryString["connectionName"];

            connections.Add(connectionName, Context.ConnectionId);

            return base.OnConnected();
	    }

        public void Subscribe(string connectionName)
        {
            connections.Add(connectionName, Context.ConnectionId);
        }
    }
}