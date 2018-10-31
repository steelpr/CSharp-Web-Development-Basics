namespace WebServer
{
    using WebServer.Application;
    using WebServer.Server.Contarcts;
    using WebServer.Server.Routing;
    using WebServer.Server.Routing.Contarcts;

    class Launcher : IRunnable
    {
        private Server.WebServerc webServer;

        private static void Main()
        {
            new Launcher().Run();
        }

        public void Run()
        {
            IApplication application = new MainApplication();
            IAppRouteConfig routeConfig = new AppRouteConfig();
            application.Start(routeConfig);

            this.webServer = new Server.WebServerc(80, routeConfig);
            this.webServer.Run();
        }
    }
}
