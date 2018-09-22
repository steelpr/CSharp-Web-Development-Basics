namespace _3.RequestParser
{
    using System.Net;
    using System.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class StartUp
    {
        static void Main(string[] args)
        {
            StringBuilder result = new StringBuilder("{0}/{1} {2} {3}  \r\n" +
                                                    "Content-Length: {4} \r\n" +
                                                    "Content-Type: text/plain \r\n \r\n" +
                                                    "{5}");

            string response = string.Empty;

            Dictionary<string, HashSet<string>> pathMethods = new Dictionary<string, HashSet<string>>();

            string[] input = Console.ReadLine()
                                    .Split('/', StringSplitOptions.RemoveEmptyEntries);

            while (input[0] != "END")
            {
                var path = input[0];
                var method = input[1];

                if (!pathMethods.ContainsKey(path))
                {
                    pathMethods.Add(path, new HashSet<string>());
                    pathMethods[path].Add(method);
                }
                else
                {
                    pathMethods[path].Add(method);
                }

                input = Console.ReadLine()
                               .Split('/', StringSplitOptions.RemoveEmptyEntries);
            }

            var request = Console.ReadLine()
                                 .Split(new char[] { '/', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var isExistRequest = IsExistRequest(pathMethods, request);

            if (isExistRequest == true)
            {
                response = string.Format(result.ToString(), request[2],
                                              request[3],
                                              (int)HttpStatusCode.OK,
                                              HttpStatusCode.OK,
                                              HttpStatusCode.OK.ToString().Count(),
                                              HttpStatusCode.OK);
            }
            else
            {
                response = string.Format(result.ToString(), request[2],
                                                  request[3],
                                                  (int)HttpStatusCode.NotFound,
                                                  HttpStatusCode.NotFound,
                                                  HttpStatusCode.NotFound.ToString().Count(),
                                                  HttpStatusCode.NotFound);
            }
            Console.WriteLine(response);
        }

        private static bool IsExistRequest(Dictionary<string, HashSet<string>> pathMethods, string[] request)
        {
            if (pathMethods.ContainsKey(request[1]))
            {
                var exist = pathMethods[request[1]].FirstOrDefault(x => x == request[0].ToLower());

                if (exist != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
