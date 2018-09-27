namespace _1.EvenNumbersThread
{
    using System;
    using System.Linq;
    using System.Threading;

    class StartUp
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine()
                                 .Split()
                                 .Select(int.Parse)
                                 .ToArray();

            int startNumber = numbers[0];
            int endNumber = numbers[1];

            Thread evens = new Thread(() => PrintEvenNumbers(startNumber, endNumber));
            evens.Start();
            evens.Join();
            Console.WriteLine("Thred finished work.");
        }

        private static void PrintEvenNumbers(int start, int end)
        {
            for (int i = start; i <= end ; i++)
            {
                var isEven = i % 2 == 0;

                if (isEven)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
