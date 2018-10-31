namespace WebServer.Server.Handlers.Contracts
{
    using Server.HTTP.Contarcts;

    public interface IRequestHandler
    {
        IHttpResponse Handle(IHttpContext httpContext);
    }
}
