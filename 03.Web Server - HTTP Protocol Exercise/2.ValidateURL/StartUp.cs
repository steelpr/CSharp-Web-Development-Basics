namespace _2.ValidateURL
{
    using System;
    using System.Net;
    using System.Text;

    class StartUp
    {
        static void Main(string[] args)
        {
            StringBuilder result = new StringBuilder("Protocol: {0} \r\n" +
                                                     "Host: {1} \r\n" +
                                                     "Port: {2} \r\n" +
                                                     "Path: {3}");

            string url = Console.ReadLine();

            string output = string.Empty;

            try
            {
                string decode = WebUtility.UrlDecode(url);

                var uri = new Uri(decode);

                var isValid = IsValid(uri);

                if (isValid == true)
                {
                    output = Output(uri, result);
                }
                else
                {
                    output = "Invalid URL";
                }

                Console.WriteLine(output.Trim());
            }
            catch (UriFormatException ex)
            {
                Console.WriteLine("Invalid URL");
            }
        }

        private static string Output(Uri uri, StringBuilder response)
        {
            var schema = uri.Scheme;
            var host = uri.Host;
            var port = uri.Port;
            var path = uri.AbsolutePath;
            var query = uri.Query.Trim('?');
            var fragment = uri.Fragment.Trim('#');

            if (query != string.Empty && fragment != string.Empty)
            {
                response.AppendLine("\r\nQuery: {4} \r\nFragment: {5}");


                return string.Format(response.ToString(), schema,
                                                          host,
                                                          port,
                                                          path,
                                                          query,
                                                          fragment);
            }
            else if (query != string.Empty && fragment == string.Empty)
            {
                response.AppendLine("\r\n Query: {4}");

                return string.Format(response.ToString(), schema,
                                                          host,
                                                          port,
                                                          path,
                                                          query);
            }
            else if (query == string.Empty && fragment != string.Empty)
            {
                response.AppendLine("\r\n Fragment: {fragment}");

                return string.Format(response.ToString(), schema,
                                                          host,
                                                          port,
                                                          path,
                                                          fragment);
            }
            else
            {
                return string.Format(response.ToString(), schema,
                                                          host,
                                                          port,
                                                          path);
            }
        }

        private static bool IsValid(Uri uri)
        {
            var schema = uri.Scheme;
            var port = uri.Port;

            if (uri.Scheme == null || uri.Host == null || uri.Port < 0 || uri.AbsolutePath == null)
            {
                return false;
            }
            else if (schema == "http" && port == 443 || schema == "https" && port == 80)
            {
                return false;
            }
            return true;
        }
    }
}
