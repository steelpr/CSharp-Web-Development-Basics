namespace WebServer.Server.HTTP.Contarcts
{
    using System.Collections.Generic;
    using Response;

    public interface IHttpHeaderCollection
    {
        void Add(HttpHeader httpHeader);

        bool ContainsKey(string key);

        ICollection<HttpHeader> GetHeader(string key);
    }
}
