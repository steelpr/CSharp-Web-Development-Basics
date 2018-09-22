namespace _1.URLDecode
{
    using System;
    using System.Net;

    class StartUp
    {
        static void Main(string[] args)
        {
            string url = Console.ReadLine();

            var webUtility =  WebUtility.UrlDecode(url);

            Console.WriteLine(webUtility);
        }
    }
}
