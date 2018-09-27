namespace _2.SliceFile
{
    using System;
    using System.IO;

    class StartUp
    {
        static void Main(string[] args)
        {
            var projectDirectory = GetProjectDirectory();

            string filePath = Console.ReadLine();

            string destinationFolder = Console.ReadLine();

            int pieces = int.Parse(Console.ReadLine());

            if (pieces <= 0)
            {
                while (pieces <= 0)
                {
                    Console.WriteLine("The number of pieces should be bigger than 0! \r\nPlease enter new number...");
                    pieces = int.Parse(Console.ReadLine());
                }
            }


            string destinationName = string.Join("", projectDirectory, destinationFolder);

            var sliceFile = new SliceFileAsync(filePath, destinationName, pieces);
            sliceFile.SliceAsync();

            Console.WriteLine("Anything else?");

            string input = string.Empty;

            while (input != "exit")
            {
                input = Console.ReadLine();
            }
        }

        private static string GetProjectDirectory()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var directoryName = Path.GetFileName(currentDirectory);
            var relativePath = directoryName.StartsWith("netcoreapp") ? @"../../../" : string.Empty;

            return relativePath;
        }
    }
}
