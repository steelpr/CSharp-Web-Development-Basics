namespace WebServer.Server.HTTP
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Contarcts;
    using Response;
    using Enums;
    using Exceptions;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.HeaderCollection = new HttpHeaderCollection();
            this.UrlParameter = new Dictionary<string, string>();
            this.QueryParameters = new Dictionary<string, string>();
            this.FormData = new Dictionary<string, string>();

            this.ParseRequest(requestString);
        }

        public Dictionary<string, string> FormData { get; set; }

        public HttpHeaderCollection HeaderCollection { get; private set; }

        public string Path { get; set; }

        public Dictionary<string, string> QueryParameters { get; }

        public HttpRequestMethod RequestMethod { get; set; }

        public string Url { get; set; }

        public Dictionary<string, string> UrlParameter { get; }

        public void AddUrlParameter(string key, string value)
        {
            this.UrlParameter[key] = value;
        }

        private void ParseRequest(string requestString)
        {
            string[] requestLines = requestString
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            string[] requestLine = requestLines[0]
                .Trim()
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (requestLine.Length != 3 || requestLine[2].ToLower() != "http/1.1")
            {
                throw new BadRequestException("Invalid request line");
            }

            this.RequestMethod = this.ParseRequestMethod(requestLine[0].ToUpper());
            this.Url = requestLine[1];
            this.Path = this.Url
                .Split(new[] { '?', '#' }, StringSplitOptions.RemoveEmptyEntries)[0];
            this.ParseHeader(requestLines);
            this.ParseParameters();

            if (this.RequestMethod == HttpRequestMethod.POST)
            {
                this.ParseQuery(requestLines[requestLines.Length - 1], this.FormData);
            }
        }

        private HttpRequestMethod ParseRequestMethod(string requestString)
        {
            try
            {
                HttpRequestMethod requestMethod = (HttpRequestMethod)Enum.Parse(typeof(HttpRequestMethod), requestString);
                return requestMethod;
            }
            catch (Exception)
            {
                throw new BadRequestException("Invalid request line!");
            }
        }

        private void ParseParameters()
        {
            if (!this.Url.Contains("?"))
            {
                return;
            }

            string query = this.Url.Split('?')[1];
            this.ParseQuery(query, this.QueryParameters);
        }

        private void ParseQuery(string query, Dictionary<string, string> queryParameters)
        {
            if (!query.Contains("="))
            {
                return;
            }

            string[] queryPairs = query.Split('&');

            foreach (var queryPair in queryPairs)
            {
                string[] queruArgs = queryPair.Split('=');

                if (queruArgs.Length != 2)
                {
                    continue;
                }

                queryParameters.Add(
                    WebUtility.UrlDecode(queruArgs[0]),
                    WebUtility.UrlDecode(queruArgs[1]));                
            }
        }

        private void ParseHeader(string[] requestLine)
        {
            int endIndex = Array.IndexOf(requestLine, string.Empty);

            for (int i = 1; i < endIndex; i++)
            {
                string[] headerArgs = requestLine[i]
                    .Split(new[] { ": " }, StringSplitOptions.None);

                var header = new HttpHeader(headerArgs[0], headerArgs[1]);

                this.HeaderCollection.Add(header);
            }

            if (!this.HeaderCollection.ContainsKey("Host"))
            {
                throw new BadRequestException("Invalid request line");
            }
        }
    }
}
