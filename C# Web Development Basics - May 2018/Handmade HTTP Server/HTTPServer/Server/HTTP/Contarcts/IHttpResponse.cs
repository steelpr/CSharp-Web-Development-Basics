namespace WebServer.Server.HTTP.Contarcts
{
    using System.Net;
    using Response;

    public interface IHttpResponse
    {
        HttpHeaderCollection HttpHeaderCollection { get; }

        HttpStatusCode StatusCode { get; }

        string StatusMessage { get; }

        string Response { get; }

        void AddHeader(string location, string redirectUrl);
    }
}
