using System;
using System.Globalization;
using System.IO;
using System.Web;

namespace ScreenPad.WebUI.Helpers
{
    public static class GameHelper
    {
        public static short GetRandomCode()
        {
            var random = new Random();
            return (short) random.Next(1000, 9999);
        }

        public static long GetHighScore(HttpServerUtilityBase server)
        {
            try
            {
                var highScoreText = File.ReadAllText(server.MapPath("~/bin/highscore.txt"));
                long highScore;
                return long.TryParse(highScoreText, out highScore) == false ? 0 : highScore;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static void SetHighScore(HttpServerUtilityBase server, long highScore)
        {
            var oldScore = GetHighScore(server);
            if (oldScore < highScore)
                File.WriteAllText(server.MapPath("~/bin/highscore.txt"), highScore.ToString(CultureInfo.InvariantCulture));
        }
    }
}