namespace WebServer.Server.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Enums;
    using Handlers;
    using Routing.Contarcts;

    public class AppRouteConfig : IAppRouteConfig
    {
        public readonly Dictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> routes;

        public IReadOnlyDictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> Routes => this.routes;

        public void AddRoute(string route, RequestHandler httpHandler)
        {
            if (httpHandler.GetType().ToString().ToLower().Contains("get"))
            {
                this.routes[HttpRequestMethod.GET].Add(route, httpHandler);
            }
            else if (httpHandler.GetType().ToString().ToLower().Contains("post"))
            {
                this.routes[HttpRequestMethod.POST].Add(route, httpHandler);
            }
        }

        public AppRouteConfig()
        {
            this.routes = new Dictionary<HttpRequestMethod, Dictionary<string, RequestHandler>>();

            foreach (HttpRequestMethod requestMethod in Enum.GetValues(typeof(HttpRequestMethod)).Cast<HttpRequestMethod>())
            {
                this.routes.Add(requestMethod, new Dictionary<string, RequestHandler>());
            }
        }
    }
}
