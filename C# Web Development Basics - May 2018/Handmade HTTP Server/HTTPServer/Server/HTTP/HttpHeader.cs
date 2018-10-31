﻿namespace WebServer.Server.HTTP.Response
{
    public class HttpHeader
    {
        public HttpHeader(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; private set; }

        public string Value { get; private set; }

        public override string ToString()
        {
            return string.Join(": ", this.Key, this.Value);
        }
    }
}
