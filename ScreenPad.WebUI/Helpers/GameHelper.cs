using System;
using System.Globalization;
using System.IO;
using System.Web;

namespace ScreenPad.WebUI.Helpers
{
    public static class GameHelper
    {
        public static string GetRandomCode()
        {
            var random = new Random();
            return random.Next(10000, 99999).ToString();
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