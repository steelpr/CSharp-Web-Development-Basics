﻿namespace WebServer.Application.Views
{
    using Server.Contarcts;

    public class NotFoundView : IView
    {
        public string View()
        {
            return "<body><center><h1>404 Not Found :(</h1></br><a href=\"/\">Home</a></center></body>";
        }
    }
}
