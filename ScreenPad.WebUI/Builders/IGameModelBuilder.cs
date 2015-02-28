using System.Web;
using ScreenPad.WebUI.Models;

namespace ScreenPad.WebUI.Builders
{
    interface IGameModelBuilder
    {
        GameModel GetGame(HttpServerUtilityBase server);
    }
}
