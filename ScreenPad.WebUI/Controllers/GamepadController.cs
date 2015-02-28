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
        // GET: /Gamepad/
        public ActionResult Index()
        {
            var viewModel = gamepadModelBuilder.GetGamepad();
            return View(viewModel);
        }

        //
        // GET: /Busy/
        public ActionResult Busy()
        {
            return View();
        }
    }
}