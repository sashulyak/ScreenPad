﻿using System.Web.Mvc;
using System.Web.Routing;

namespace ScreenPad.WebUI
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "GamepadBusy",
                "gamepad/busy",
                new { controller = "Gamepad", action = "Busy" }
            );
            
            routes.MapRoute(
                "Gamepad",
                "gamepad",
                new { controller = "Gamepad", action = "Index" }
            );
            
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}