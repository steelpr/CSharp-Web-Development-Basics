namespace WebServer.Server.Routing
{
    using System.Collections.Generic;
    using Handlers;
    using Routing.Contarcts;

    public class RoutingContext : IRoutingContext
    {
        public RoutingContext(RequestHandler requestHandler, IEnumerable<string> parameters)
        {
            Parameters = parameters;
            RequestHandler = requestHandler;
        }

        public IEnumerable<string> Parameters { get; private set; }

        public RequestHandler RequestHandler { get; set; }
    }
}
