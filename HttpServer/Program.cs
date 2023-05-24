using Common;
using HttpServer.Controllers;

namespace HttpServer
{
    public class Program
    {
        public static void Main()
        {
            Thread.CurrentThread.IsBackground = true;
            var builder = WebApplication.CreateBuilder();

            var app = builder.Build();

            app.UsePathBase("/");
            app.Urls.Add($"http://*:{Global.config.Http.HttpPort}");
            app.Urls.Add($"https://*:{Global.config.Http.HttpsPort}");

            DispatchController.AddHandlers(app);

            app.Run();
        }
    }
}