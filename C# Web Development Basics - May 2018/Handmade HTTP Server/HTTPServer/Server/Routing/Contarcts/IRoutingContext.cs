namespace WebServer.Server.Routing.Contarcts
{
    using System.Collections.Generic;
    using Handlers;

    public interface IRoutingContext
    {
        IEnumerable<string> Parameters { get; }

        RequestHandler RequestHandler { get; }
    }
}
