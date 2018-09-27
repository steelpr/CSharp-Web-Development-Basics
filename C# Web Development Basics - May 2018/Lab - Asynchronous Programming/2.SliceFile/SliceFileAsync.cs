namespace _2.SliceFile
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class SliceFileAsync
    {
        public SliceFileAsync(string sourceFile, string destinationPath, int pices)
        {
            SourceFile = sourceFile;
            DestinationPath = destinationPath;
            Pieces = pices;
        }

        public string SourceFile { get; set; }

        public string DestinationPath { get; set; }

        public int Pieces { get; set; }

        public void Slice()
        {
            if (!Directory.Exists(this.DestinationPath))
            {
                Directory.CreateDirectory(this.DestinationPath);
            }

            try
            {
                using (var source = new FileStream(this.SourceFile, FileMode.Open))
                {
                    FileInfo file = new FileInfo(this.SourceFile);

                    long partLength = (source.Length / this.Pieces) + 1;
                    long currentByte = 0;
                    long bufferLength = partLength;

                    for (int currentPart = 1; currentPart <= this.Pieces; currentPart++)
                    {
                        string filePath = $"{this.DestinationPath}/Part{currentPart}{file.Extension}";

                        using (var destination = new FileStream(filePath, FileMode.Create))
                        {
                            byte[] buffer = new byte[bufferLength];

                            while (currentByte < partLength * currentPart)
                            {
                                int readByteCounts = source.Read(buffer, 0, buffer.Length);

                                if (readByteCounts == 0)
                                {
                                    break;
                                }

                                destination.Write(buffer, 0, readByteCounts);
                                currentByte += readByteCounts;
                            }
                        }

                        Console.WriteLine($"Part {currentPart}/{this.Pieces} split was successful.");
                    }

                    Console.WriteLine("Slice complete.");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File does not exist.");
            }

        }

        public void SliceAsync()
        {
            Task.Run(() => Slice());
        }
    }
}
