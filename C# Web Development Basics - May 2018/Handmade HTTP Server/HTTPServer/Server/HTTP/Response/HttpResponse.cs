namespace WebServer.Server.HTTP.Response
{
    using System.Net;
    using System.Text;
    using Contarcts;
    using Server.Contarcts;

    public abstract class HttpResponse : IHttpResponse
    {
        private readonly IView view;

        protected HttpResponse(string redirectUrl)
        {
            this.HttpHeaderCollection = new HttpHeaderCollection();
            this.StatusCode = HttpStatusCode.Found;
            this.AddHeader("Locaion", redirectUrl);
        }

        protected HttpResponse(HttpStatusCode responseCode, IView view)
        {
            this.HttpHeaderCollection = new HttpHeaderCollection();
            this.view = view;
            this.StatusCode = responseCode;
        }

        public HttpHeaderCollection HttpHeaderCollection { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string StatusMessage => this.StatusCode.ToString();

        public void AddHeader(string key, string value)
        {
            this.HttpHeaderCollection.Add(new HttpHeader(key, value));
        }

        public string Response
        {
            get
            {
                var response = new StringBuilder();

                var statusCodeNumber = (int)this.StatusCode;
                response.AppendLine($"HTTP/1.1 {statusCodeNumber} {this.StatusMessage}");

                response.AppendLine(this.HttpHeaderCollection.ToString());
                response.AppendLine();

                if ((int)this.StatusCode < 300 || (int) this.StatusCode > 400)
                {
                    response.AppendLine(this.view.View());
                }

                return response.ToString();
            }
        }
    }
}
