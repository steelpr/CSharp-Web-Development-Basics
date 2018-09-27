namespace _3.SimpleWebServer
{
    using System.Threading.Tasks;

    class StartUp
    {
        static void Main(string[] args)
        {
            var httpServer = new HttpServer();

            Task.Run(async () => await httpServer.StartAsync())
                .GetAwaiter()
                .GetResult();
        }
    }
}
