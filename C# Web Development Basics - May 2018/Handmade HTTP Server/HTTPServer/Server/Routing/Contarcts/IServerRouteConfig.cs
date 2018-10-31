namespace WebServer.Server.Routing.Contarcts
{
    using System.Collections.Generic;
    using Enums;

    public interface IServerRouteConfig
    {
        Dictionary<HttpRequestMethod, Dictionary<string, IRoutingContext>> Routes { get; }
    }
}
