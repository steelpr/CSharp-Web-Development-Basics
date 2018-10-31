namespace WebServer.Server.Exceptions
{
    using System;

    public class BadRequestException : Exception
    {

        public BadRequestException()
            : base()
        {

        }

        public BadRequestException(string exception)
            : base(exception)
        {

        }
    }
}
