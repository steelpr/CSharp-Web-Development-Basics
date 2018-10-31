namespace WebServer.Server.Contarcts
{
    using Routing.Contarcts;

    public interface IApplication
    {
        void Start(IAppRouteConfig appRouteConfig);
    }
}
