namespace WebServer.Server.HTTP.Response
{
    using System.Collections.Generic;
    using Contarcts;
    using System;
    using System.Text;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly Dictionary<string, ICollection<HttpHeader>> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, ICollection<HttpHeader>>();
        }

        public void Add(HttpHeader httpHeader)
        {
            if (!this.headers.ContainsKey(httpHeader.Key))
            {
                this.headers[httpHeader.Key] = new List<HttpHeader>();
            }

            this.headers[httpHeader.Key].Add(httpHeader);
        }

        public bool ContainsKey(string key)
        {
            return this.headers.ContainsKey(key);
        }

        public ICollection<HttpHeader> GetHeader(string key)
        {
            if (!this.headers.ContainsKey(key))
            {
                throw new ArgumentException($"The given key \"{key}\" does not exist!");
            }

            return this.headers[key];
        }

        public override string ToString()
        {
            StringBuilder header = new StringBuilder();

            foreach (var headerKey in headers)
            {
                var key = headerKey.Key;

                foreach (var headerValue in headerKey.Value)
                {
                    header.AppendLine(string.Join(": ", key, headerValue.Value));
                }
            }

            return header.ToString().Trim();
        }
    }
}
