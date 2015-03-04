using System.Web;
using ScreenPad.WebUI.Helpers;
using ScreenPad.WebUI.Models;

namespace ScreenPad.WebUI.Builders
{
    public class GameModelBuilder : IGameModelBuilder
    {
        public GameModel GetGame(HttpServerUtilityBase server)
        {
            return new GameModel
                {
                    ConnectionName = GameHelper.GetRandomCode(),
                    HighScore = GameHelper.GetHighScore(server)
                };
        }
    }
}