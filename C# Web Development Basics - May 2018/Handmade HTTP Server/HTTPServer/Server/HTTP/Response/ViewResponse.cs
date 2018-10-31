namespace WebServer.Server.HTTP.Response
{
    using System.Net;
    using Server.Contarcts;

    public class ViewResponse : HttpResponse
    {
        public ViewResponse(HttpStatusCode responseCode, IView view)
            : base(responseCode, view)
        {
        }
    }
}
