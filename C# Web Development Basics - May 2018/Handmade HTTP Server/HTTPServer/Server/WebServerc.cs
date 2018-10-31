namespace WebServer.Server
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;
    using Contarcts;
    using Routing;
    using Routing.Contarcts;

    public class WebServerc : IRunnable
    {
        private readonly int port;

        private readonly IServerRouteConfig serverRouteConfig;

        private readonly TcpListener tcpListener;

        private bool isRunning;

        public WebServerc(int port, IAppRouteConfig routeConfig)
        {
            this.port = port;
            this.tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);

            this.serverRouteConfig = new ServerRouteConfig(routeConfig);
        }

        public void Run()
        {
            this.tcpListener.Start();

            this.isRunning = true;

            Console.WriteLine($"Server started. Listener to TCP clientat 127.0.0.1:{port}");

            Task task = Task.Run(this.ListenLoop);
            task.Wait();
        }

        private async Task ListenLoop()
        {
            while (this.isRunning)
            {
                Socket client = await this.tcpListener.AcceptSocketAsync();
                ConnectionHandler connectionHandler = new ConnectionHandler(client, this.serverRouteConfig);
                Task connection = connectionHandler.ProcessRequestAsync();
                connection.Wait();
            }
        }
    }
}
