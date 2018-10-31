namespace WebServer.Application.Controllers
{
    using System.Net;
    using Views;
    using Server.HTTP.Contarcts;
    using Server.HTTP.Response;

    public class HomeController
    {
        public IHttpResponse Index()
        {
            return new ViewResponse(HttpStatusCode.OK, new HomeIndexView());
        }
    }
}
