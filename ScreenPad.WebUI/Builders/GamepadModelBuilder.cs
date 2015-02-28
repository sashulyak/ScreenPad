using ScreenPad.WebUI.Helpers;
using ScreenPad.WebUI.Models;

namespace ScreenPad.WebUI.Builders
{
    public class GamepadModelBuilder : IGamepadModelBuilder
    {
        public GamepadModel GetGamepad()
        {
            return new GamepadModel
                {
                    Id = GameHelper.GetRandomCode()
                };
        }
    }
}