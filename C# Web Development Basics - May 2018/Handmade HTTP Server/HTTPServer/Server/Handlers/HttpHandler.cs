namespace WebServer.Server.Handlers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Text.RegularExpressions;
    using Handlers.Contracts;
    using HTTP.Response;
    using Routing.Contarcts;
    using Server.HTTP.Contarcts;
    using Application.Views;

    public class HttpHandler : IRequestHandler
    {
        private readonly IServerRouteConfig serverRouteConfig;

        public HttpHandler(IServerRouteConfig serverRouteConfig)
        {
            this.serverRouteConfig = serverRouteConfig;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            foreach (KeyValuePair<string, IRoutingContext> kvp in this.serverRouteConfig.Routes[httpContext.Request.RequestMethod])
            {
                string pattern = "/";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(httpContext.Request.Path);

                if (!match.Success)
                {
                    continue;
                }

                foreach (string parameter in kvp.Value.Parameters)
                {
                    httpContext.Request.AddUrlParameter(parameter, match.Groups[parameter].Value);
                }

                return kvp.Value.RequestHandler.Handle(httpContext);
            }

            return new ViewResponse(HttpStatusCode.NotFound, new NotFoundView());
        }
    }
}
