using System.Web.Mvc;
using ScreenPad.WebUI.Builders;

namespace ScreenPad.WebUI.Controllers
{
    public class GamepadController : Controller
    {
        private readonly IGamepadModelBuilder gamepadModelBuilder;
        
        public GamepadController()
        {
            gamepadModelBuilder = new GamepadModelBuilder();
        }

        //
        // GET: /Gamepad/{gameConnectionName}
        public ActionResult Index(string gameConnectionName)
        {
            var viewModel = gamepadModelBuilder.GetGamepad(gameConnectionName);
            return View(viewModel);
        }
    }
}