namespace WebServer.Server.Handlers
{
    using System;
    using Server.HTTP.Contarcts;

    public class GETHandler : RequestHandler
    {
        public GETHandler(Func<IHttpContext, IHttpResponse> func)
            : base(func)
        {
        }
    }
}
