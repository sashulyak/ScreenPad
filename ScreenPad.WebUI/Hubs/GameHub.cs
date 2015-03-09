using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace ScreenPad.WebUI.Hubs
{
	public class GameHub : Hub
	{
        private readonly static IConnectionMapping<string> Connections = new ConnectionMapping<string>();

        public void Send(string message, string who)
		{
            foreach (var connectionId in Connections.GetConnections(who))
            {
                Clients.Client(connectionId).addNewMessageToPage(message);
            }
        }

        public override Task OnConnected()
	    {
	        var connectionName = Context.Request.QueryString["connectionName"];

            Connections.Add(connectionName, Context.ConnectionId);

            return base.OnConnected();
	    }

        public void Subscribe(string connectionName)
        {
            Connections.Add(connectionName, Context.ConnectionId);
        }
    }
}