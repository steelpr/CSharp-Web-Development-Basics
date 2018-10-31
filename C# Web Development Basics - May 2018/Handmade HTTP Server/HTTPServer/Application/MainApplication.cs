namespace WebServer.Application
{
    using Controllers;
    using Server.Contarcts;
    using Server.Handlers;
    using Server.Routing.Contarcts;

    public class MainApplication : IApplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AddRoute("/", new GETHandler(httpContext => new HomeController().Index()));
        }
    }
}
