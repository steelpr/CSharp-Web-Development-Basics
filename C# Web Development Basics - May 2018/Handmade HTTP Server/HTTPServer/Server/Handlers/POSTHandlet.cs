namespace WebServer.Server.Handlers
{
    using System;
    using HTTP.Contarcts;

    public class POSTHandlet : RequestHandler
    {
        public POSTHandlet(Func<IHttpContext, IHttpResponse> func)
            : base(func)
        {
        }
    }
}
