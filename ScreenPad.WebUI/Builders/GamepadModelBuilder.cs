using ScreenPad.WebUI.Models;

namespace ScreenPad.WebUI.Builders
{
    public class GamepadModelBuilder : IGamepadModelBuilder
    {
        public GamepadModel GetGamepad(string gameConnectionName)
        {
            return new GamepadModel
                {
                    GameConnectionName = gameConnectionName
                };
        }
    }
}