using Owin;

namespace ScreenPad.WebUI
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// Any connection or hub wire up and configuration should go here
			app.MapSignalR();
		}
	}
}