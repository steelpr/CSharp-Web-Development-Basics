namespace WebServer.Application.Views
{
    using Server.Contarcts;
    using System.IO;

    public class HomeIndexView : IView
    {
        public string View()
        {
            return File.ReadAllText(@"..\..\..\..\HTTPServer\HTMLAndCSS\index.html");
        }
    }
}
