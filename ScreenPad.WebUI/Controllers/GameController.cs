using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using ScreenPad.WebUI.Builders;
using ScreenPad.WebUI.Helpers;

namespace ScreenPad.WebUI.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameModelBuilder gameModelBuilder;

        public GameController()
        {
            gameModelBuilder = new GameModelBuilder();
        }

        //
        // GET: /Game/
        public ActionResult Index()
        {
            var viewModel = gameModelBuilder.GetGame(Server);
            return View("Index", viewModel);
        }

        public ActionResult LogOn()
        {
            return View("LogOn");
        }

        public ActionResult QrCode(string connectionName)
        {
            var qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode;
            var text = Url.Action("Index", "Gamepad", null, Request.Url.Scheme, null);
            qrEncoder.TryEncode(text, out qrCode);
            var renderer = new GraphicsRenderer(new FixedModuleSize(24, QuietZoneModules.Four), Brushes.Black, new SolidBrush(Color.White));
            Stream memoryStream = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, memoryStream);
            memoryStream.Position = 0;
            var resultStream = new FileStreamResult(memoryStream, "image/png")
                {
                    FileDownloadName = String.Format("{0}.png", text)
                };

            return resultStream;
        }

        [HttpPost]
        public JsonResult SetHighScore(long highScore)
        {
            try
            {
                GameHelper.SetHighScore(Server, highScore);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }
    }
}
