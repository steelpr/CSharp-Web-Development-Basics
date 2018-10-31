namespace WebServer.Server.HTTP.Contarcts
{
    public interface IHttpContext
    {
        IHttpRequest Request { get; }
    }
}
