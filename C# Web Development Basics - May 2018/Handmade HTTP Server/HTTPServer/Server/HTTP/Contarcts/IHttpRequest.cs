namespace WebServer.Server.HTTP.Contarcts
{
    using System.Collections.Generic;
    using Response;
    using Server.Enums;

    public interface IHttpRequest
    {
        Dictionary<string, string> FormData { get; }

        HttpHeaderCollection HeaderCollection { get; }

        string Path { get; }

        Dictionary<string, string> QueryParameters { get; }

        HttpRequestMethod RequestMethod { get; }

        string Url { get; }

        Dictionary<string, string> UrlParameter { get; }

        void AddUrlParameter(string key, string value);
    }
}
