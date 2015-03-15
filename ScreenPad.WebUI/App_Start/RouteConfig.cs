using System.Web.Mvc;
using System.Web.Routing;

namespace ScreenPad.WebUI
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "GamepadGameOver",
                "gamepad/gameover",
                new { controller = "Gamepad", action = "GameOver" }
            );
            
            routes.MapRoute(
                "Gamepad",
                "gamepad/{gameConnectionName}",
                new { controller = "Gamepad", action = "Index" }
            );

            routes.MapRoute(
                "Game",
                "game",
                new { controller = "Game", action = "Index" }
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}