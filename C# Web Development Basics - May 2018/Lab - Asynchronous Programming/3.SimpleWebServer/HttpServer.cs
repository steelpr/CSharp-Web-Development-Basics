namespace _3.SimpleWebServer
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class HttpServer
    {
        private TcpListener tcpListener;
        private IPAddress ipAddress;
        private const int port = 1300;

        public HttpServer()
        {
            this.ipAddress = IPAddress.Parse("127.0.0.1");
            this.tcpListener = new TcpListener(ipAddress, port);
        }

        public async Task StartAsync()
        {
            this.tcpListener.Start();

            Console.WriteLine("Server started.");
            Console.WriteLine($"Listening to TCP clients at 127.0.0.1:{port}");

            while (true)
            {
                Console.WriteLine("Waiting for client...");
                var client = await tcpListener.AcceptTcpClientAsync();

                Console.WriteLine("Client connected.");

                byte[] buffer = new byte[1024];

                await client.GetStream().ReadAsync(buffer, 0, buffer.Length);

                var message = Encoding.ASCII.GetString(buffer);
                Console.WriteLine(message);

                string hello = "HTTP/1.1 200 OK\nContent-Type: text/plain\n\nHello from server!";
                byte[] date = Encoding.ASCII.GetBytes(hello);

                await client.GetStream().WriteAsync(date, 0, date.Length);

                Console.WriteLine("Closing connection.");
                client.GetStream().Dispose();
            }
        }
    }
}
