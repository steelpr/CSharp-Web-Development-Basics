﻿namespace WebServer.Server.Enums
{
    public enum HttpResponseStatusCode
    {
        OK = 200,
        MovedPermanently = 301,
        Found = 302,
        MoveTemporarily = 303,
        NotAuthorized = 401,
        NotFound = 404,
        InternalServerError = 500,
    }
}
