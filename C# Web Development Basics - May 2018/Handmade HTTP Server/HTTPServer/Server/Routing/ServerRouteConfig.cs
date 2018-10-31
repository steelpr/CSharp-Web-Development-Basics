namespace WebServer.Server.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Enums;
    using Handlers;
    using Routing.Contarcts;

    public class ServerRouteConfig : IServerRouteConfig
    {
        public ServerRouteConfig(IAppRouteConfig appRouteConfig)
        {
            this.Routes = new Dictionary<HttpRequestMethod, Dictionary<string, IRoutingContext>>();

            foreach (HttpRequestMethod requestMethod in Enum.GetValues(typeof(HttpRequestMethod)).Cast<HttpRequestMethod>())
            {
                this.Routes.Add(requestMethod, new Dictionary<string, IRoutingContext>());
            }

            this.InitializeServerConfig(appRouteConfig);
        }

        public Dictionary<HttpRequestMethod, Dictionary<string, IRoutingContext>> Routes { get; set; }

        private void InitializeServerConfig(IAppRouteConfig appRouteConfig)
        {
            foreach (KeyValuePair<HttpRequestMethod, Dictionary<string, RequestHandler>> kvp in appRouteConfig.Routes)
            {
                foreach (KeyValuePair<string, RequestHandler> requestHandler in kvp.Value)
                {
                    List<string> args = new List<string>();

                    string parsedRegex = this.ParseRoute(requestHandler.Key, args);

                    IRoutingContext routingContext = new RoutingContext(requestHandler.Value, args);

                    this.Routes[kvp.Key].Add(parsedRegex, routingContext);
                }
            }
        }

        private string ParseRoute(string requestHandledKey, List<string> args)
        {
            StringBuilder parsedRegex = new StringBuilder();
            parsedRegex.Append('*');

            if (requestHandledKey == "/")
            {
                parsedRegex.Append($"{requestHandledKey}$");
                return parsedRegex.ToString();
            }

            string[] tokens = requestHandledKey.Split('/');

            this.ParseTokens(args, tokens, parsedRegex);

            return parsedRegex.ToString();
        }

        private void ParseTokens(List<string> args, string[] tokens, StringBuilder parsedRegex)
        {
            for (int index = 0; index < tokens.Length; index++)
            {
                string end = index == tokens.Length - 1 ? "$" : "/";
                if (!tokens[index].StartsWith("{") && !tokens[index].EndsWith("}"))
                {
                    parsedRegex.Append($"{tokens[index]}{end}");
                    continue;
                }

                string pattern = "<\\w+>";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(tokens[index]);


                if (!match.Success)
                {
                    continue;
                }

                string paramName = match.Groups[0].Value.Substring(1, match.Groups[0].Length - 2);
                args.Add(paramName);
                parsedRegex.Append($"{tokens[index].Substring(1, tokens[index].Length - 2)}{end}");
            }
        }

    }
}
